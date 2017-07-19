﻿using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan2Ext
{

    internal static class TheConfiguration
    {
        private static string _CurrentConfig = string.Empty;
        private const int RTNORM = 5100;
        private static Dictionary<string, ConfigVar> _ConfigDict = new Dictionary<string, ConfigVar>();
        private static bool _Loaded = false;
        private static Encoding _Encoding = Encoding.Default;

        public static bool Loaded
        {
            get { return _Loaded; }
            set { _Loaded = value; }
        }

        public static string GetValueString(string name)
        {
            Type type;
            object value;
            GetValue(name, out type, out value);
            return value.ToString();
        }

        public static bool GetValue(string name, out Type type, out object value)
        {
            if (!_Loaded)
            {
                LoadConfig();
            }

            ConfigVar cv;
            if (_ConfigDict.TryGetValue(name, out cv))
            {
                cv.GetValue(out type,out  value);
                return true;
            }
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Konfigurationseintrag für Configvar {0} nicht gefunden!", name));
        }

        private static void LoadConfig()
        {
            _Loaded = true;
            GetEncoding();
            GetConfiguration();
            ReadVals();

        }
        private static void ReadVals()
        {
            string[] Lines = File.ReadAllLines(_CurrentConfig, _Encoding);
            for (int i = 0; i < Lines.Length; i++)
            {
                string Line = Lines[i];
                ConfigVar oConfigVar = null;
                try
                {
                    oConfigVar = new ConfigVar(Line);

                    _ConfigDict[oConfigVar.VarName] = oConfigVar;
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException(string.Format("Fehler in Konfiguration '{0}', Zeile {1};\n{2}", _CurrentConfig, i + 1, ex.Message));
                }
            }

        }

        private static void GetEncoding()
        {

            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding e = ei.GetEncoding();
                if (ei.CodePage == 1252) { _Encoding = e; return; }
            }
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Konnte Encoding für Ansi nicht finden!"));
        }


        private static void GetConfiguration()
        {
            _CurrentConfig = string.Empty;
            using (var rb = new ResultBuffer(new TypedValue((int)LispDataType.Text, "c:Plan2CurrentConfig")))
            {
                int stat = 0;
                ResultBuffer res = CADDZone.AutoCAD.Samples.AcedInvokeSample.InvokeLisp(rb, ref stat);
                if (stat == RTNORM && res != null)
                {
                    _CurrentConfig = res.AsArray()[0].Value.ToString();
                    res.Dispose();
                }
                else
                {
                    throw new InvalidOperationException("Konnte Konfigurationsdateiname nicht ermitteln!");
                }
            }
        }

    }

    internal class Category
    {
        public string Name { get; set; }
        public List<ConfigVar> ConfigVars = new List<ConfigVar>();
        public Category(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
        public IEnumerable<string> AsLines()
        {
            return ConfigVars.Select(x => x.ToLine());
        }
    }

    internal class ConfigVar
    {
        #region Constants
        public const string TRENN = "|";
        #endregion

        public string Category { get; set; }
        public string Description { get; set; }
        public string VarName { get; set; }
        public string Value { get; set; }
        public string VarType { get; set; }

        public ConfigVar(string line)
        {
            string[] array = line.Split(new string[] { TRENN }, StringSplitOptions.None);
            if (array.Length != 5) throw new InvalidOperationException(string.Format("Ungültige Zeile in Konfiguration: '{0}'!", line));

            Category = array[0];
            Description = array[1];
            VarName = array[2];
            Value = array[3];
            VarType = array[4];
        }


        internal string ToLine()
        {
            return Category + TRENN + Description + TRENN + VarName + TRENN + Value + TRENN + VarType;
        }

        internal void GetValue(out Type type, out object value)
        {
            switch (VarType)
            {
                case "String":
                    type = typeof(string);
                    value = Value;
                    break;
                case "Real":
                    type = typeof(double);
                    value = double.Parse(Value, CultureInfo.InvariantCulture);
                    break;
                case "Int":
                    type = typeof(int);
                    value = int.Parse(Value, CultureInfo.InvariantCulture);
                    break;
                default:
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Ungültiger Typ in Configvar {0}\\{1}: '{2}'!", Category, Description, VarType));
            }
        }



    }


}
