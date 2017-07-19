﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if BRX_APP
using Bricscad.ApplicationServices;
#elif ARX_APP
using Autodesk.AutoCAD.ApplicationServices;
#endif

namespace Plan2Ext.Raumnummern
{
    internal static class Globs
    {
        public static RnOptions TheRnOptions = null;

        public static void CancelCommand()
        {
            Application.DocumentManager.MdiActiveDocument.SendStringToExecute("\x1B", true, false, true);
            Application.DocumentManager.MdiActiveDocument.SendStringToExecute("\x1B", true, false, true);
        }
    }
}
