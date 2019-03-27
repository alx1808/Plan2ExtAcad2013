﻿// ReSharper disable CommentTypo
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
using _AcInt = BricscadApp;
using Bricscad.Windows;
#elif ARX_APP
using _AcAp = Autodesk.AutoCAD.ApplicationServices;
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
using _AcInt = Autodesk.AutoCAD.Interop;
using Autodesk.AutoCAD.Windows;
#endif

using System;
using System.Collections.Generic;
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace Plan2Ext.Vorauswahl
{
    public class VorauswahlPalette
    {
        // We cannot derive from PaletteSet
        // so we contain it
        static PaletteSet _ps;

        // We need to make the textbox available
        // via a static member
        static VorauswahlControl _userControl;

        public VorauswahlPalette()
        {
            _userControl = new VorauswahlControl();
        }

        public bool Show()
        {

            if (_ps == null)
            {
                _ps = new PaletteSet("Vorauswahl")
                {
                    Style = PaletteSetStyles.NameEditable |
                            PaletteSetStyles.ShowPropertiesMenu |
                            PaletteSetStyles.ShowAutoHideButton |
                            PaletteSetStyles.ShowCloseButton,
                    MinimumSize = new System.Drawing.Size(140, 235)
                };
#if ACAD2013_OR_NEWER
#if ARX_APP
                _ps.SetSize(new System.Drawing.Size(154, 235));
#endif
#endif
                _ps.Add("Vorauswahl", _userControl);
                if (!_ps.Visible)
                {
                    _ps.Visible = true;
                }
                return false;
            }
            else
            {
                if (!_ps.Visible)
                {
                    _ps.Visible = true;
                    return false;
                }
                return true;
            }
        }

        public List<string> BlocknamesInList()
        {
            var blockNames = new List<string>();
            foreach (var item in _userControl.lstBlocknamen.Items)
            {
                blockNames.Add(item.ToString());
            }
            return blockNames;
        }

        public void AddBlockNamesToList(List<string> blockNames)
        {
            if (blockNames == null) return;
            foreach (var blockName in blockNames)
            {
                _userControl.lstBlocknamen.Items.Add(blockName);
            }
        }

        public void AddBlockNameToList(string blockName)
        {
            if (string.IsNullOrEmpty(blockName)) return;
            _userControl.lstBlocknamen.Items.Add(blockName);
        }

        public List<string> LayernamesInList()
        {
            var layerNames = new List<string>();
            foreach (var item in _userControl.lstLayer.Items)
            {
                layerNames.Add(item.ToString());
            }
            return layerNames;
        }

        public IEnumerable<Type> EntityTypesInList()
        {
            var entityTypes = new List<Type>();
            foreach (var item in _userControl.lstEntityTypes.Items)
            {
                var entityItem = (EntityTypeItem)item;
                entityTypes.Add(entityItem.Type);
            }

            return entityTypes;
        }

        public void AddLayerNameToList(string layerName)
        {
            if (string.IsNullOrEmpty(layerName)) return;
            _userControl.lstLayer.Items.Add(layerName);
        }

        public void SetResultTextTo(string s)
        {
            _userControl.lblResult.Text = s;
        }
    }
}
