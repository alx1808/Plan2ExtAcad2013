using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using System.Globalization;
using Plan2Ext.Configuration;

namespace Plan2Ext
{
    public class Config
    {

        #region log4net Initialization
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Convert.ToString((typeof(Config))));
        static Config()
        {
            if (log4net.LogManager.GetRepository(System.Reflection.Assembly.GetExecutingAssembly()).Configured == false)
            {
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(System.IO.Path.Combine(new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName, "_log4net.config")));
            }

        }
        #endregion

        #region Member Variables
        private static string _FileName = "";
        #endregion

        [LispFunction("NetSetPlan2Config")]
        public static ResultBuffer NetSetPlan2Config(ResultBuffer rb)
        {
            try
            {
                TheConfiguration.Loaded = false;

                List<string> existingConfigs = new List<string>();
                string current = "";
                if (!GetArgs2(rb, ref current, existingConfigs))
                {
                    log.Error("No valid configlist!");

                }

                using (SetConfigForm frm = new SetConfigForm(current, existingConfigs))
                {
                    System.Windows.Forms.DialogResult res = Application.ShowModalDialog(frm);
                    if (res == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return null;
                    }
                    else
                    {
                        return new ResultBuffer(new TypedValue((int)Autodesk.AutoCAD.Runtime.LispDataType.Text, frm.Configuration));
                    }

                }

            }
            catch (System.Exception ex)
            {
                Application.ShowAlertDialog(string.Format(CultureInfo.CurrentCulture, "Fehler in NetSetPlan2Config aufgetreten!\n{0}", ex.Message));

            }
            return null;
        }

        private static bool GetArgs2(ResultBuffer rb, ref string current, List<string> configs)
        {
            if (rb == null) return false;
            TypedValue[] values = rb.AsArray();
            if (values.Length <= 2) return false;

            current = values[1].Value.ToString();

            for (int i = 2; i < (values.Length - 1); i++)
            {
                var v = values[i];
                configs.Add(v.Value.ToString());
            }

            return true;

        }


        [LispFunction("ConfigPlan2")]
        public static ResultBuffer ConfigPlan2(ResultBuffer rb)
        {
            try
            {
                TheConfiguration.Loaded = false;

                GetArgs(rb);

                using (ConfigForm frm = new ConfigForm(_FileName))
                {
                    Application.ShowModalDialog(frm);
                }

            }
            catch (System.Exception ex)
            {
                Application.ShowAlertDialog(string.Format(CultureInfo.CurrentCulture, "Fehler in ConfigPlan2 aufgetreten!\n{0}", ex.Message));

            }
            return null;
        }

        private static void GetArgs(ResultBuffer rb)
        {
            TypedValue[] values = rb.AsArray();
            _FileName = values[0].Value.ToString();
        }

    }



}
