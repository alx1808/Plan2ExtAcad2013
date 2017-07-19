using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

#if BRX_APP
using _AcAp = Bricscad.ApplicationServices;
//using _AcBr = Teigha.BoundaryRepresentation;
using _AcCm = Teigha.Colors;
using _AcDb = Teigha.DatabaseServices;
using _AcEd = Bricscad.EditorInput;
using _AcGe = Teigha.Geometry;
using _AcGi = Teigha.GraphicsInterface;
using _AcGs = Teigha.GraphicsSystem;
using _AcPl = Bricscad.PlottingServices;
using _AcBrx = Bricscad.Runtime;
using _AcTrx = Teigha.Runtime;
using _AcWnd = Bricscad.Windows;
using _AcIntCom = BricscadDb;
#elif ARX_APP
using _AcAp = Autodesk.AutoCAD.ApplicationServices;
using _AcBr = Autodesk.AutoCAD.BoundaryRepresentation;
using _AcCm = Autodesk.AutoCAD.Colors;
using _AcDb = Autodesk.AutoCAD.DatabaseServices;
using _AcEd = Autodesk.AutoCAD.EditorInput;
using _AcGe = Autodesk.AutoCAD.Geometry;
using _AcGi = Autodesk.AutoCAD.GraphicsInterface;
using _AcGs = Autodesk.AutoCAD.GraphicsSystem;
using _AcPl = Autodesk.AutoCAD.PlottingServices;
using _AcBrx = Autodesk.AutoCAD.Runtime;
using _AcTrx = Autodesk.AutoCAD.Runtime;
using _AcWnd = Autodesk.AutoCAD.Windows;
using _AcIntCom = Autodesk.AutoCAD.Interop.Common;
#endif

namespace Plan2Ext
{
    internal class CenterRaumBlock
    {
        #region log4net Initialization
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Convert.ToString((typeof(CenterRaumBlock))));
        #endregion

        #region Constants
        private const double ABSTANDTEXT = 2.0;

        #endregion

        #region Member variables


        private string _RaumblockName = "";
        private string _FgLayer = "";
        private List<_AcDb.ObjectId> _FlaechenGrenzen = new List<_AcDb.ObjectId>();
        private List<_AcDb.ObjectId> _Raumbloecke = new List<_AcDb.ObjectId>();

        #endregion



        internal void DoIt(_AcAp.Document doc, string rbName, string fgLayer)
        {
            log.Debug("--------------------------");

            _FlaechenGrenzen.Clear();
            _Raumbloecke.Clear();

            var _AreaEngine = new AreaEngine();

            _AcGe.Matrix3d ucs = _AcGe.Matrix3d.Identity;
            try
            {
                ucs = doc.Editor.CurrentUserCoordinateSystem;
                doc.Editor.CurrentUserCoordinateSystem = _AcGe.Matrix3d.Identity;

                if (!string.IsNullOrEmpty(rbName)) _RaumblockName = rbName;
                if (!string.IsNullOrEmpty(fgLayer)) _FgLayer = fgLayer;

                _AreaEngine.SelectFgAndRb(_FlaechenGrenzen, _Raumbloecke, _FgLayer, _RaumblockName);

                if (_FlaechenGrenzen.Count == 0) return;

                // todo: läuft nicht synchron - wird dzt in lisp ausgeführt
                //Globs.SetWorldUCS();

                ZoomToFlaechenGrenzen();

                // init div
                int fehlerKeinRb = 0;
                int fehlerMehrRb = 0;
                int fehlerWertFalsch = 0;

                _AcDb.Database db = doc.Database;
                _AcEd.Editor ed = doc.Editor;
                _AcDb.TransactionManager tm = db.TransactionManager;
                _AcDb.Transaction myT = tm.StartTransaction();
                try
                {
                    _AcGe.Point2d lu = new _AcGe.Point2d();
                    _AcGe.Point2d ro = new _AcGe.Point2d();

                    for (int i = 0; i < _FlaechenGrenzen.Count; i++)
                    {
                        log.Debug("--------------------------");

                        double sumAF = 0;
                        int rbInd = -1;
                        _AcDb.ObjectId elFG = _FlaechenGrenzen[i];
                        log.DebugFormat("Flächengrenze {0}", elFG.Handle.ToString());

                        _AcDb.Extents3d ext = GetExtents(tm, elFG);
                        _AcGe.Point3d minExt = new _AcGe.Point3d(ext.MinPoint.X - ABSTANDTEXT, ext.MinPoint.Y - ABSTANDTEXT, ext.MinPoint.Z);
                        _AcGe.Point3d maxExt = new _AcGe.Point3d(ext.MaxPoint.X + ABSTANDTEXT, ext.MaxPoint.Y + ABSTANDTEXT, ext.MaxPoint.Z);

                        List<_AcDb.ObjectId> rbsToIgnoreCol = GetFgAnz(minExt, maxExt, elFG);
                        if (rbsToIgnoreCol.Count > 0)
                        {
                            string handles = string.Join(",", rbsToIgnoreCol.Select(x => x.Handle.ToString()).ToArray());
                            log.DebugFormat("Zu ignorierende Raumblöcke: {0}", handles);
                        }

                        //    'raumbloecke holen
                        List<_AcDb.ObjectId> ssRB = selRB(minExt, maxExt);
                        if (ssRB.Count > 0)
                        {
                            string handles = string.Join(",", ssRB.Select(x => x.Handle.ToString()).ToArray());
                            log.DebugFormat("Raumblöcke: {0}", handles);

                        }


                        int rbAnz = 0;
                        //    'raumbloecke pruefen
                        for (int rbCnt = 0; rbCnt < ssRB.Count; rbCnt++)
                        {
                            _AcDb.ObjectId rbBlock2 = ssRB[rbCnt];

                            //      ' ignore rbs
                            _AcDb.ObjectId found = rbsToIgnoreCol.FirstOrDefault(x => x.Equals(rbBlock2));
                            if (found != default(_AcDb.ObjectId)) continue;

                            using (_AcDb.DBObject dbObj = tm.GetObject(rbBlock2, _AcDb.OpenMode.ForRead, false))
                            {
                                _AcGe.Point3d rbEp = ((_AcDb.BlockReference)dbObj).Position;

                                using (_AcDb.Entity elFGEnt = (_AcDb.Entity)tm.GetObject(elFG, _AcDb.OpenMode.ForRead, false))
                                {
                                    if (AreaEngine.InPoly(rbEp, elFGEnt))
                                    {
                                        log.DebugFormat("Raumblock {0} ist innerhalb der Flächengrenze.", rbBlock2.Handle.ToString());

                                        if (_Raumbloecke.Contains(rbBlock2)) _Raumbloecke.Remove(rbBlock2);
                                        rbAnz++;
                                        rbInd = rbCnt;
                                    }
                                    else
                                    {
                                        log.DebugFormat("Außen liegender Raumblock {0} wird ignoriert.", rbBlock2.Handle.ToString());
                                    }

                                }
                            }
                        }


                        if (rbAnz < 1)
                        {
                            log.WarnFormat("Kein Raumblock in Flächengrenze {0}!", elFG.Handle.ToString());
                            //FehlerLineOrHatchPoly(elFG, _InvalidNrRb, 255, 0, 0, tm, Globs.GetLabelPoint(elFG));
                            fehlerKeinRb++;

                        }
                        else if (rbAnz > 1)
                        {
                            log.WarnFormat("Mehr als ein Raumblock in Flächengrenze {0}!", elFG.Handle.ToString());
                            //FehlerLineOrHatchPoly(elFG, _InvalidNrRb, 255, 0, 0, tm, Globs.GetLabelPoint(elFG));
                            fehlerMehrRb++;

                        }
                        else
                        {
                            using (var tr = doc.TransactionManager.StartTransaction())
                            {
                                var pt = Globs.GetLabelPoint(elFG);
                                if (pt.HasValue)
                                {
                                    var rblock = tr.GetObject(ssRB[rbInd], _AcDb.OpenMode.ForWrite) as _AcDb.BlockReference;

                                    var pos = rblock.GetCenter();
                                    if (!pos.HasValue)
                                    {
                                        pos = rblock.Position;
                                    }
                                    _AcGe.Vector3d acVec3d = pos.Value.GetVectorTo(pt.Value);
                                    rblock.TransformBy(_AcGe.Matrix3d.Displacement(acVec3d));
                                    ed.WriteMessage("\nCentroid is {0}", pt);
                                }
                                else
                                {
                                    var poly = tr.GetObject(elFG, _AcDb.OpenMode.ForRead) as _AcDb.Polyline;
                                    string msg = string.Format(CultureInfo.CurrentCulture, "\nFläche {0}. Centroid liegt außerhalb.", poly.Handle.ToString());
                                    ed.WriteMessage(msg);
                                    log.Warn(msg);
                                }

                                tr.Commit();
                            }
                        }
                    }

                    //if (_Raumbloecke.Count > 0)
                    //{
                    //    List<object> insPoints = new List<object>();
                    //    for (int i = 0; i < _Raumbloecke.Count; i++)
                    //    {
                    //        _AcIntCom.AcadBlockReference rbBlock = (_AcIntCom.AcadBlockReference)Globs.ObjectIdToAcadEntity(_Raumbloecke[i], tm);
                    //        insPoints.Add(rbBlock.InsertionPoint);
                    //    }

                    //    _AcCm.Color col = _AcCm.Color.FromRgb((byte)0, (byte)255, (byte)0);

                    //    Plan2Ext.Globs.InsertFehlerLines(insPoints, _LooseBlockLayer, 50, Math.PI * 1.25, col);

                    //}



                    if (fehlerKeinRb > 0 || fehlerMehrRb > 0 || fehlerWertFalsch > 0 || _Raumbloecke.Count > 0)
                    {
                        string msg = string.Format(CultureInfo.CurrentCulture, "Räume ohne Raumblock: {0}\nRäume mit mehr als einem Raumblock: {1}\nRäume mit falschem Wert in Raumblock: {2}\nRaumblöcke ohne entsprechende Flächengrenzen: {3}", fehlerKeinRb, fehlerMehrRb, fehlerWertFalsch, _Raumbloecke.Count);
                        log.Debug(msg);
                        _AcAp.Application.ShowAlertDialog(msg);

                    }

                    //If wucs = 0 Then
                    //    ThisDrawing.SendCommand "(command ""_.UCS"" ""_P"") "
                    //End If

                    myT.Commit();

                }
                finally
                {
                    myT.Dispose();
                }

            }
            finally
            {
                doc.Editor.CurrentUserCoordinateSystem = ucs;
            }
        }

        private void ZoomToFlaechenGrenzen()
        {
            log.DebugFormat(CultureInfo.CurrentCulture, "Zoom auf Flächengrenzen");
            if (_FlaechenGrenzen.Count == 0) return;

            double MinX, MinY, MaxX, MaxY;

            _AcAp.Document doc = _AcAp.Application.DocumentManager.MdiActiveDocument;
            _AcDb.Database db = doc.Database;
            _AcEd.Editor ed = doc.Editor;
            _AcDb.TransactionManager tm = db.TransactionManager;
            _AcDb.Transaction myT = tm.StartTransaction();
            try
            {

                _AcDb.Extents3d ext = GetExtents(tm, _FlaechenGrenzen[0]);
                MinX = ext.MinPoint.X;
                MinY = ext.MinPoint.Y;
                MaxX = ext.MaxPoint.X;
                MaxY = ext.MaxPoint.Y;

                for (int i = 1; i < _FlaechenGrenzen.Count; i++)
                {
                    _AcDb.ObjectId oid = _FlaechenGrenzen[i];
                    ext = GetExtents(tm, oid);
                    if (ext.MinPoint.X < MinX) MinX = ext.MinPoint.X;
                    if (ext.MinPoint.Y < MinY) MinY = ext.MinPoint.Y;
                    if (ext.MaxPoint.X > MaxX) MaxX = ext.MaxPoint.X;
                    if (ext.MaxPoint.Y > MaxY) MaxY = ext.MaxPoint.Y;
                }


                //Globs.Zoom( new Point3d(MinX, MinY, 0.0), new Point3d(MaxX, MaxY, 0.0),new Point3d(), 1.0);
                //Globs.ZoomWin3(ed, new Point3d(MinX, MinY, 0.0), new Point3d(MaxX, MaxY, 0.0));
                //Globs.ZoomWin2(ed, new Point3d(MinX, MinY, 0.0), new Point3d(MaxX, MaxY, 0.0));

                myT.Commit();

            }
            finally
            {
                myT.Dispose();
            }

            // Rauszoomen, sonst werden Blöcken nicht gefunden, die außerhalb der Flächengrenzen liegen.
            MinX -= ABSTANDTEXT;
            MinY -= ABSTANDTEXT;
            MaxX += ABSTANDTEXT;
            MaxY += ABSTANDTEXT;

            Globs.Zoom(new _AcGe.Point3d(MinX, MinY, 0.0), new _AcGe.Point3d(MaxX, MaxY, 0.0), new _AcGe.Point3d(), 1.0);

        }

        private static _AcDb.Extents3d GetExtents(_AcDb.TransactionManager tm, _AcDb.ObjectId oid)
        {
            using (_AcDb.DBObject dbobj = tm.GetObject(oid, _AcDb.OpenMode.ForRead, false))
            {
                _AcDb.Entity ent = dbobj as _AcDb.Entity;
                return ent.GeometricExtents;
            }
        }

        private List<_AcDb.ObjectId> GetFgAnz(_AcGe.Point3d minExt, _AcGe.Point3d maxExt, _AcDb.ObjectId elFG)
        {
            List<_AcDb.ObjectId> Ret = new List<_AcDb.ObjectId>();

            _AcEd.Editor ed = _AcAp.Application.DocumentManager.MdiActiveDocument.Editor;
            _AcEd.SelectionFilter filter = new _AcEd.SelectionFilter(new _AcDb.TypedValue[] { 
                new _AcDb.TypedValue((int)_AcDb.DxfCode.Start,"*POLYLINE" ),
                new _AcDb.TypedValue((int)_AcDb.DxfCode.LayerName,_FgLayer )
            });
            _AcEd.PromptSelectionResult res = null;
            res = ed.SelectCrossingWindow(minExt, maxExt, filter);
            //res = ed.SelectAll(filter);
            if (res.Status != _AcEd.PromptStatus.OK)
            {
                // todo: logging: lot4net?
                return Ret;
            }

#if BRX_APP
            _AcEd.SelectionSet ss = res.Value;
#else
            using (_AcEd.SelectionSet ss = res.Value)
#endif
            {

                _AcDb.ObjectId[] idArray = ss.GetObjectIds();
                _AcDb.Database db = _AcAp.Application.DocumentManager.MdiActiveDocument.Database;
                _AcDb.TransactionManager tm = db.TransactionManager;
                _AcDb.Transaction myT = tm.StartTransaction();
                try
                {
                    for (int i = 0; i < idArray.Length; i++)
                    {
                        _AcDb.ObjectId oid = idArray[i];
                        if (!oid.Equals(elFG))
                        {
                            if (PolyInPoly(tm, oid, elFG))
                                AddRbToRetCol(Ret, tm, oid);
                        }
                    }
                    myT.Commit();
                }
                finally
                {
                    myT.Dispose();
                }

            }

            return Ret;

        }

        private static bool PolyInPoly(_AcDb.TransactionManager tm, _AcDb.ObjectId oid, _AcDb.ObjectId elFG)
        {
            using (_AcDb.DBObject pEntity = tm.GetObject(oid, _AcDb.OpenMode.ForRead, false))
            {
                using (_AcDb.DBObject pElFG = tm.GetObject(elFG, _AcDb.OpenMode.ForRead, false))
                {

                    if (pEntity is _AcDb.Polyline2d)
                    {
                        _AcDb.Polyline2d oldPolyline = (_AcDb.Polyline2d)pEntity;
                        foreach (_AcDb.ObjectId Vertex2d in oldPolyline)
                        {
                            using (_AcDb.DBObject dbobj = tm.GetObject(Vertex2d, _AcDb.OpenMode.ForRead, false))
                            {
                                _AcDb.Vertex2d vertex = dbobj as _AcDb.Vertex2d;

                                if (vertex == null)
                                {
                                    string msg = string.Format(CultureInfo.CurrentCulture, "Polylinie {0} gibt falsches Objekt {1} als Vertex zurück.", oldPolyline.Handle.ToString(), dbobj.GetType().ToString());
                                    throw new InvalidOperationException(string.Format(msg));
                                }

                                _AcGe.Point3d vertexPoint = oldPolyline.VertexPosition(vertex);
                                if (!AreaEngine.InPoly(vertexPoint, (_AcDb.Entity)pElFG)) return false;

                            }
                        }
                        return true;
                    }
                    else if (pEntity is _AcDb.Polyline3d)
                    {
                        _AcDb.Polyline3d poly3d = (_AcDb.Polyline3d)pEntity;
                        foreach (_AcDb.ObjectId Vertex3d in poly3d)
                        {
                            using (_AcDb.DBObject dbobj = tm.GetObject(Vertex3d, _AcDb.OpenMode.ForRead, false))
                            {
                                _AcDb.PolylineVertex3d vertex = dbobj as _AcDb.PolylineVertex3d;

                                if (vertex == null)
                                {
                                    string msg = string.Format(CultureInfo.CurrentCulture, "3D-Polylinie {0} gibt falsches Objekt {1} als Vertex zurück.", poly3d.Handle.ToString(), dbobj.GetType().ToString());
                                    throw new InvalidOperationException(string.Format(msg));
                                }

                                _AcGe.Point3d vertexPoint = vertex.Position;
                                if (!AreaEngine.InPoly(vertexPoint, (_AcDb.Entity)pElFG)) return false;

                            }
                        }
                        return true;
                    }
                    else if (pEntity is _AcDb.Polyline)
                    {
                        _AcDb.Polyline poly = pEntity as _AcDb.Polyline;
                        for (int i = 0; i < poly.NumberOfVertices; i++)
                        {
                            _AcGe.Point3d vertexPoint = poly.GetPoint3dAt(i);
                            if (!AreaEngine.InPoly(vertexPoint, (_AcDb.Entity)pElFG)) return false;

                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private void AddRbToRetCol(List<_AcDb.ObjectId> Ret, _AcDb.TransactionManager tm, _AcDb.ObjectId elFG)
        {

            _AcDb.Extents3d ext = GetExtents(tm, elFG);
            _AcGe.Point3d minExt = new _AcGe.Point3d(ext.MinPoint.X - ABSTANDTEXT, ext.MinPoint.Y - ABSTANDTEXT, ext.MinPoint.Z);
            _AcGe.Point3d maxExt = new _AcGe.Point3d(ext.MaxPoint.X + ABSTANDTEXT, ext.MaxPoint.Y + ABSTANDTEXT, ext.MaxPoint.Z);

            _AcEd.Editor ed = _AcAp.Application.DocumentManager.MdiActiveDocument.Editor;
            _AcEd.SelectionFilter filter = new _AcEd.SelectionFilter(new _AcDb.TypedValue[] { 
                new _AcDb.TypedValue((int)_AcDb.DxfCode.Start,"INSERT" ),
                new _AcDb.TypedValue((int)_AcDb.DxfCode.BlockName,_RaumblockName)
            });
            _AcEd.PromptSelectionResult res = null;
            res = ed.SelectCrossingWindow(minExt, maxExt, filter);
            if (res.Status != _AcEd.PromptStatus.OK)
            {
                // todo: logging: lot4net?
                return;
            }

#if BRX_APP
            _AcEd.SelectionSet ss = res.Value;
#else
            using (_AcEd.SelectionSet ss = res.Value)
#endif
            {


                _AcDb.ObjectId[] idArray = ss.GetObjectIds();
                for (int i = 0; i < idArray.Length; i++)
                {
                    _AcDb.ObjectId oid = idArray[i];
                    using (_AcDb.DBObject pEntity = tm.GetObject(oid, _AcDb.OpenMode.ForRead, false))
                    {
                        using (_AcDb.Entity entElFG = tm.GetObject(elFG, _AcDb.OpenMode.ForRead, false) as _AcDb.Entity)
                        {
                            if (pEntity is _AcDb.BlockReference)
                            {
                                _AcDb.BlockReference br = pEntity as _AcDb.BlockReference;
                                if (AreaEngine.InPoly(br.Position, entElFG))
                                {
                                    Ret.Add(oid);
                                }
                            }
                        }
                    }
                }
            }
        }

        private List<_AcDb.ObjectId> selRB(_AcGe.Point3d minExt, _AcGe.Point3d maxExt)
        {
            _AcEd.Editor ed = _AcAp.Application.DocumentManager.MdiActiveDocument.Editor;
            _AcEd.SelectionFilter filter = new _AcEd.SelectionFilter(new _AcDb.TypedValue[] { 
                new _AcDb.TypedValue((int)_AcDb.DxfCode.Start,"INSERT" ),
                new _AcDb.TypedValue((int)_AcDb.DxfCode.BlockName,_RaumblockName)
            });
            _AcEd.PromptSelectionResult res = null;
            res = ed.SelectCrossingWindow(minExt, maxExt, filter);
            if (res.Status != _AcEd.PromptStatus.OK)
            {
                log.Warn("Fehler beim Auswählen der Raumblöcke!");
                return new List<_AcDb.ObjectId>();
            }

#if BRX_APP
            _AcEd.SelectionSet ss = res.Value;
#else
            using (_AcEd.SelectionSet ss = res.Value)
#endif
            {

                return ss.GetObjectIds().ToList();
            }

        }
    }
}
