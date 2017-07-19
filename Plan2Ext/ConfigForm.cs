using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;



namespace Plan2Ext
{
    public partial class ConfigForm : Form
    {
        #region Member Variables
        private string _FileName = "";
        private Dictionary<string, Category> _Categories = new Dictionary<string, Category>();
        private Encoding _Encoding = Encoding.Default;

        #endregion

        #region Constants
        #endregion

        #region Lifecycle
        public ConfigForm(string fileName)
        {
            InitializeComponent();
            _FileName = fileName;

            if (!string.IsNullOrEmpty(_FileName))
            {
                this.Text = "Konfiguration: " + _FileName;
            }
        }
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            GetEncoding();
            ReadVals();
            FillCombo();

        }

        private void GetEncoding()
        {
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding e = ei.GetEncoding();
                if (ei.CodePage == 1252) { _Encoding = e; return; }
            }
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Konnte Encoding für Ansi nicht finden!"));
        }
        
        #endregion

        #region Controls

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListVals();
        }

        private void lstVars_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstVals.SelectedIndex = lstVars.SelectedIndex;
            txtWert.Text = ((Category)cmbCategory.SelectedItem).ConfigVars[lstVals.SelectedIndex].Value;
        }

        private void txtWert_Validating(object sender, CancelEventArgs e)
        {
            Category cat = cmbCategory.SelectedItem as Category;
            if (cat == null) return;
            if (lstVals.SelectedIndex == -1) return;
            ConfigVar cv = cat.ConfigVars[lstVals.SelectedIndex];
            switch (cv.VarType.ToUpperInvariant())
            {
                case "REAL":
                    double testdouble;
                    if (!double.TryParse(txtWert.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out testdouble))
                    {
                        string msg = string.Format(CultureInfo.CurrentCulture, "Text '{0}' hat nicht den Datentyp REAL!", txtWert.Text);
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(msg);
                        e.Cancel = true;
                        return;
                    }
                    cv.Value = testdouble.ToString(CultureInfo.InvariantCulture);
                    break;
                case "INT":
                    int testint;
                    if (!int.TryParse(txtWert.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out testint))
                    {
                        string msg = string.Format(CultureInfo.CurrentCulture, "Text '{0}' hat nicht den Datentyp REAL!", txtWert.Text);
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(msg);
                        e.Cancel = true;
                        return;
                    }
                    cv.Value = testint.ToString(CultureInfo.InvariantCulture);
                    break;
                case "STRING":
                    if (txtWert.Text.Contains(ConfigVar.TRENN))
                    {
                        string msg = string.Format(CultureInfo.CurrentCulture, "Text darf nicht das Zeichen '{0}' enthalten!", ConfigVar.TRENN);
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(msg);
                        e.Cancel = true;
                        return;
                    }
                    cv.Value = txtWert.Text;
                    break;
                default:
                    string msg2 = string.Format(CultureInfo.CurrentCulture, "Ungültiger Datentyp '{0}'!", cv.VarType);
                    Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(msg2);
                    break;
            }

            UpdateListVals();

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            WriteVals();
            this.Close();
        }

        #endregion

        #region Private Methods
        private void FillCombo()
        {
            cmbCategory.Items.Clear();
            foreach (Category cat in _Categories.Values)
            {
                cmbCategory.Items.Add(cat);
            }
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
        }

        private void ReadVals()
        {
            _Categories.Clear();
            string[] Lines = File.ReadAllLines(_FileName,_Encoding );
            for (int i = 0; i < Lines.Length; i++)
            {
                string Line = Lines[i];
                ConfigVar oConfigVar = null;
                try
                {
                    oConfigVar = new ConfigVar(Line);
                }
                catch (InvalidOperationException ex)
                {
                    throw new InvalidOperationException(string.Format("Fehler in Konfiguration '{0}', Zeile {1};\n{2}", _FileName, i + 1, ex.Message));
                }

                Category cat = null;
                if (!_Categories.TryGetValue(oConfigVar.Category, out cat))
                {
                    cat = new Category(oConfigVar.Category);
                    _Categories.Add(cat.Name, cat);
                }
                cat.ConfigVars.Add(oConfigVar);


            }

        }

        private void UpdateListVals()
        {
            lstVars.Items.Clear();
            lstVals.Items.Clear();

            Category cat = cmbCategory.SelectedItem as Category;
            if (cat == null) return;
            foreach (ConfigVar cv in cat.ConfigVars)
            {
                lstVars.Items.Add(cv.Description);
                lstVals.Items.Add(cv.Value);
            }
        }

        private void WriteVals()
        {
            string[] test = _Categories.Values.SelectMany(x => x.AsLines()).ToArray();
            File.WriteAllLines(_FileName, test,_Encoding  );
        }
        
        #endregion


    }
}
