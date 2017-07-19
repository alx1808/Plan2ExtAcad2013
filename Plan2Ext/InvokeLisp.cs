using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.EditorInput;
using System.Runtime.InteropServices;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

// Sample: calling acedInvoke from managed code.
// 
// To call LISP functions via acedInvoke, they must be 
// defined with a C: prefix, or they must be registered 
// using (vl-acad-defun)
//
// The test code below below requires the following LISP:
// 
// (defun testfunc ( a b )
// (princ "\nTESTFUNC called: )
// (prin1 a)
// (prin1 b)
// ;; return a list of results:
// (list 99 "Just ditch the LISP and forget this nonsense" pi)
// )
// 
// ;; register the function to be externally callable:
// 
// (vl-acad-defun 'testfunc)
// 
// 

namespace CADDZone.AutoCAD.Samples
{
    public class AcedInvokeSample
    {
        public const int RTLONG = 5010; // adscodes.h
        public const int RTSTR = 5005;

        public const int RTNORM = 5100;

        public AcedInvokeSample()
        {
        }

#if ACAD2013_OR_NEWER

        const String IMPORT_FILE_NAME = "accore.dll";
#else
            const String IMPORT_FILE_NAME = "acad.exe";
#endif

        [System.Security.SuppressUnmanagedCodeSecurity]
        [DllImport(IMPORT_FILE_NAME, CallingConvention = CallingConvention.Cdecl)]
        extern static private int acedInvoke(IntPtr args, out IntPtr result);

        // Helper for acedInvoke()

        public static ResultBuffer InvokeLisp(ResultBuffer args, ref int stat)
        {
            IntPtr rb = IntPtr.Zero;
            stat = acedInvoke(args.UnmanagedObject, out rb);
            if (stat == (int)PromptStatus.OK && rb != IntPtr.Zero)
                return (ResultBuffer)
               DisposableWrapper.Create(typeof(ResultBuffer), rb, true);
            return null;
        }

        static void PrintResbuf(ResultBuffer rb)
        {
            string s = "\n-----------------------------";
            foreach (TypedValue val in rb)
                s += string.Format("\n{0} -> {1}", val.TypeCode,
               val.Value.ToString());
            s += "\n-----------------------------";
            AcadApp.DocumentManager.MdiActiveDocument.
            Editor.WriteMessage(s);
        }

        public static void InvokeCLisp(string funcNameWithoutC)
        {
            ResultBuffer args = new ResultBuffer();
            int stat = 0;

            args.Add(new TypedValue(RTSTR, "c:" + funcNameWithoutC ));

            ResultBuffer res = InvokeLisp(args, ref stat);
            if (stat == RTNORM && res != null)
            {
                PrintResbuf(res);
                res.Dispose();
            }
        }

        public static void InvokeCGeoin()
        {
            ResultBuffer args = new ResultBuffer();
            int stat = 0;

            args.Add(new TypedValue(RTSTR, "c:geoin"));

            ResultBuffer res = InvokeLisp(args, ref stat);
            if (stat == RTNORM && res != null)
            {
                PrintResbuf(res);
                res.Dispose();
            }
        }

        public static void TestInvokeLisp()
        {
            ResultBuffer args = new ResultBuffer();
            int stat = 0;

            args.Add(new TypedValue(RTSTR, "testfunc"));
            args.Add(new TypedValue(RTLONG, 100));
            args.Add(new TypedValue(RTLONG, 200));

            ResultBuffer res = InvokeLisp(args, ref stat);
            if (stat == RTNORM && res != null)
            {
                PrintResbuf(res);
                res.Dispose();
            }
        }
    }
}

//-- 
//http://www.caddzone.com

//AcadXTabs: MDI Document Tabs for AutoCAD 2004/2005/2006
// http://www.acadxtabs.com
