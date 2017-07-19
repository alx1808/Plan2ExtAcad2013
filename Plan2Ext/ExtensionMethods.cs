using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan2Ext
{
    public static class ExtensionMethods
    {
        public static double Distance2dTo(this Point3d point, Point3d otherPoint)
        {
            return Math.Sqrt(Math.Pow((otherPoint.X - point.X), 2.0) + Math.Pow((otherPoint.Y - point.Y), 2.0));
        }
    }
}
