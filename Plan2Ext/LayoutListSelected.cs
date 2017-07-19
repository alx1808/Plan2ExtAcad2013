using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

//[assembly: CommandClass(typeof(Plan2Ext.LayoutListSelected.Commands))]

namespace Plan2Ext.LayoutListSelected
{
    public class Commands
    {
        [LispFunction("LayoutListSelected")]
        public ResultBuffer LayoutListSelected(ResultBuffer args)
        {
            if (args != null)
                throw new TooFewArgsException();

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            //Editor ed = doc.Editor;
            //LayoutManager layoutMgr = LayoutManager.Current;
            List<string> layouts = new List<string>();
            ResultBuffer res = new ResultBuffer();

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                DBDictionary layoutDic =
                    (DBDictionary)tr.GetObject(db.LayoutDictionaryId, OpenMode.ForRead, openErased: false);

                foreach (DBDictionaryEntry entry in layoutDic)
                {
                    Layout layout =
                        (Layout)tr.GetObject(entry.Value, OpenMode.ForRead);

                    string layoutName = layout.LayoutName;

                    if (layout.TabSelected)
                        layouts.Add(layoutName);
                }

                tr.Commit();
            }

            layouts.Remove("Model");

            if (0 < layouts.Count)
            {
                layouts.Sort();

                foreach (string layoutName in layouts)
                {
                    res.Add(new TypedValue((int)(Autodesk.AutoCAD.Runtime.LispDataType.Text), layoutName));
                }

                return res;
            }

            else
                return null;
        }
    }

    // Special thanks to Gile for his LispException classes:
    class LispException : System.Exception
    {
        public LispException(string msg) : base(msg) { }
    }

     class TooFewArgsException : LispException
     {
        public TooFewArgsException() : base("too few arguments") { }
     }

     class TooManyArgsException : LispException
    {
        public TooManyArgsException() : base("too many arguments") { }
     }

     class ArgumentTypeException : LispException
     {
         public ArgumentTypeException(string s, TypedValue tv)
             : base(string.Format(
            "invalid argument type: {0}: {1}",
            s, tv.TypeCode == (int)LispDataType.Nil ? "nil" : tv.Value))
        { }
     }
}

