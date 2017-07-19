using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;

namespace Plan2Ext.Fenster
{
    public partial class FensterOptionsControl : UserControl
    {
        private FensterOptions _FensterOptions;

        internal FensterOptionsControl(FensterOptions fensterOptions)
        {
            InitializeComponent();

            _FensterOptions = fensterOptions;

            FillComponents();

        }

        private void FillComponents()
        {
            this.txtWidth.Text = _FensterOptions.BreiteString;
            this.txtHeight.Text = _FensterOptions.HoeheString;
            this.txtParapet.Text = _FensterOptions.ParapetString;
            this.txtOlAb.Text = _FensterOptions.OlAbString;
            this.txtStaerke.Text = _FensterOptions.StaerkeString;
            this.txtStock.Text = _FensterOptions.StockString;

        }

        internal void SetFensterOptions(FensterOptions fensterOptions)
        {
            _FensterOptions = fensterOptions;
            FillComponents();
        }

        private bool txtWidth_Shield = false;
        private void txtWidth_Validating(object sender, CancelEventArgs e)
        {
            if (txtWidth_Shield) return;
            try
            {
                txtWidth_Shield = true;
                _FensterOptions.BreiteString = txtWidth.Text;
                txtWidth.Text = _FensterOptions.BreiteString;

            }
            catch (Exception ex)
            {
               Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(string.Format(CultureInfo.CurrentCulture, "Fehler in FensterOptions aufgetreten! {0}", ex.Message));
            }
            finally
            {
                txtWidth_Shield = false;
            }
        }

        private bool txtHeight_Shield = false;
        private void txtHeight_Validating(object sender, CancelEventArgs e)
        {
            if (txtHeight_Shield) return;
            try
            {
                txtHeight_Shield = true;
                _FensterOptions.HoeheString= txtHeight.Text;
                txtHeight.Text = _FensterOptions.HoeheString;

            }
            catch (Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(string.Format(CultureInfo.CurrentCulture, "Fehler in FensterOptions aufgetreten! {0}", ex.Message));
            }
            finally
            {
                txtHeight_Shield = false;
            }

        }

        private bool txtParapet_Shield = false;
        private void txtParapet_Validating(object sender, CancelEventArgs e)
        {
            if (txtParapet_Shield) return;
            try
            {
                txtParapet_Shield = true;
                _FensterOptions.ParapetString= txtParapet.Text;
                txtParapet.Text = _FensterOptions.ParapetString;

            }
            catch (Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(string.Format(CultureInfo.CurrentCulture, "Fehler in FensterOptions aufgetreten! {0}", ex.Message));
            }
            finally
            {
                txtParapet_Shield = false;
            }
        }

        private bool txtOlAb_Shield = false;
        private void txtOlAb_Validating(object sender, CancelEventArgs e)
        {
            if (txtOlAb_Shield) return;
            try
            {
                txtOlAb_Shield = true;
                _FensterOptions.OlAbString= txtOlAb.Text;
                txtOlAb.Text = _FensterOptions.OlAbString;

            }
            catch (Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(string.Format(CultureInfo.CurrentCulture, "Fehler in FensterOptions aufgetreten! {0}", ex.Message));
            }
            finally
            {
                txtOlAb_Shield = false;
            }
        }

        private bool txtStaerke_Shield = false;
        private void txtStaerke_Validating(object sender, CancelEventArgs e)
        {
            if (txtStaerke_Shield) return;
            try
            {
                txtStaerke_Shield = true;
                _FensterOptions.StaerkeString= txtStaerke.Text;
                txtStaerke.Text = _FensterOptions.StaerkeString;

            }
            catch (Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(string.Format(CultureInfo.CurrentCulture, "Fehler in FensterOptions aufgetreten! {0}", ex.Message));
            }
            finally
            {
                txtStaerke_Shield = false;
            }
        }

        private bool txtStock_Shield = false;
        private void txtStock_Validating(object sender, CancelEventArgs e)
        {
            if (txtStock_Shield) return;
            try
            {
                txtStock_Shield = true;
                _FensterOptions.StockString= txtStock.Text;
                txtStock.Text = _FensterOptions.StockString;

            }
            catch (Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(string.Format(CultureInfo.CurrentCulture, "Fehler in FensterOptions aufgetreten! {0}", ex.Message));
            }
            finally
            {
                txtStock_Shield = false;
            }
        }

        private void btnSelWidth_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentCollection dm = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
                Document doc = dm.MdiActiveDocument;
                Editor ed = doc.Editor;
#if NEWSETFOCUS
                doc.Window.Focus();
#else
                Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView(); // previous 2014 AutoCAD - Versions
#endif




                PromptDoubleResult  per = ed.GetDistance("\nFensterbreite zeigen: ");

                //DocumentLock loc = dm.MdiActiveDocument.LockDocument();
                //using (loc)
                //{
                //}

                if (per.Status == PromptStatus.OK)
                {

                    _FensterOptions.Breite = per.Value;
                    txtWidth.Text = _FensterOptions.BreiteString;

                }

            }
            catch (Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.Message);
            }

        }


        private void butSelHeight_Click(object sender, EventArgs e)
        {
            try
            {
                double height = 0.0;
                if (GetHeightFromVermBlocks(ref height))
                {
                    _FensterOptions.Hoehe = height;
                    txtHeight.Text = _FensterOptions.HoeheString;

                }

            }
            catch (Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.Message);
            }
            
        }

        private void btnSelParapet_Click(object sender, EventArgs e)
        {
            try
            {
                double height = 0.0;
                if (GetHeightFromVermBlocks(ref height))
                {
                    _FensterOptions.Parapet= height;
                    txtParapet.Text  = _FensterOptions.ParapetString;

                }

            }
            catch (Exception ex)
            {
                Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(ex.Message);
            }

        }


        private bool GetHeightFromVermBlocks(ref double height)
        {
            DocumentCollection dm = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;
            Document doc = dm.MdiActiveDocument;
            Editor ed = doc.Editor;

#if NEWSETFOCUS
            doc.Window.Focus();
#else
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView(); // previous 2014 AutoCAD - Versions
#endif


            PromptEntityResult per = ed.GetEntity("\nErsten Vermessungsblock wählen: ");

            //DocumentLock loc = dm.MdiActiveDocument.LockDocument();
            //using (loc)
            //{
            //}

            bool blockFound = false;
            double height1 = 0.0;
            double height2 = 0.0;
            if (per.Status == PromptStatus.OK)
            {

                Transaction tr = doc.TransactionManager.StartTransaction();
                using (tr)
                {
                    DBObject obj = tr.GetObject(per.ObjectId, OpenMode.ForRead);
                    
                    BlockReference br = obj as BlockReference;
                    if (br == null) return false;

                    if (br.Name == "GEOINOVA")
                    {
                        blockFound = true;
                        height1 = br.Position.Z;
                        br.Highlight();
                    }

                    if (blockFound)
                    {
                        blockFound = false;
                        per = ed.GetEntity("\nZweiten Vermessungsblock wählen: ");
                        if (per.Status == PromptStatus.OK)
                        {
                                obj = tr.GetObject(per.ObjectId, OpenMode.ForRead);
                                BlockReference br2 = obj as BlockReference;
                                if (br2 == null) return false;

                                if (br2.Name == "GEOINOVA")
                                {
                                    blockFound = true;
                                    height2 = br2.Position.Z;

                                }

                        }

                        if (blockFound)
                        {
                            height = Math.Abs(height1 - height2);
                        }

                        br.Unhighlight();

                    }

                    tr.Commit();
                }

                if (!blockFound) return false;
                return true;

            }

            return false;
        }

        private void FensterOptionsControl_Load(object sender, EventArgs e)
        {

        }


    }
}
