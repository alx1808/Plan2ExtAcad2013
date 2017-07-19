using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan2Ext.AutoIdVergabe
{
    internal static class Globs
    {
        public static AutoIdOptions TheAutoIdOptions = null;

        public static void CancelCommand()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute("\x1B", true, false, true);
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute("\x1B", true, false, true);
        }

    }
}
