using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plan2Ext.Fenster
{
    internal class FensterOptions
    {
        private double _Breite = 1.0;
        public double Breite
        {
            get { return _Breite; }
            set {
                if (value > 0.0) _Breite = value; }
        }
        public string BreiteString
        {
            get {
                return (_Breite * 100.0).ToString(CultureInfo.InvariantCulture );
            }
            set
            {
                double val;
                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                {
                    Breite = (val / 100.0);
                }
            }
        }

        private double _Hoehe = 1.0;
        public double Hoehe
        {
            get { return _Hoehe; }
            set { if (value > 0.0) _Hoehe = value; }
        }
        public string HoeheString
        {
            get
            {
                return (_Hoehe * 100.0).ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                double val;
                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                {
                    Hoehe = (val / 100.0);
                }
            }
        }


        private double _Parapet = 1.0;
        public double Parapet
        {
            get { return _Parapet; }
            set { if (value > 0.0) _Parapet = value; }
        }
        public string ParapetString
        {
            get
            {
                return (_Parapet * 100.0).ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                double val;
                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                {
                    Parapet = (val / 100.0);
                }
            }
        }


        private double _OlAb = 1.5;
        public double OlAb
        {
            get { return _OlAb; }
            set { _OlAb = value; }
        }
        public string OlAbString
        {
            get
            {
                return (_OlAb * 100.0).ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                double val;
                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                {
                    OlAb = (val / 100.0);
                }
            }
        }



        private double _Staerke = 0.07;
        public double Staerke
        {
            get { return _Staerke; }
            set { if (value > 0.0) _Staerke = value; }
        }
        public string StaerkeString
        {
            get
            {
                return (_Staerke * 100.0).ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                double val;
                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                {
                    Staerke = (val / 100.0);
                }
            }
        }


        private double _Stock = 0.06;
        public double Stock
        {
            get { return _Stock; }
            set { if (value > 0.0) _Stock = value; }
        }
        public string StockString
        {
            get
            {
                return (_Stock * 100.0).ToString(CultureInfo.InvariantCulture);
            }
            set
            {
                double val;
                if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                {
                    Stock = (val / 100.0);
                }
            }
        }

        public ResultBuffer AsResultBuffer()
        {
            return new ResultBuffer(new TypedValue(5001, _Breite),
                new TypedValue(5001, _Hoehe ),
                new TypedValue(5001, _Parapet ),
                new TypedValue(5001, _OlAb),
                new TypedValue(5001, _Staerke),
                new TypedValue(5001, _Stock));
        }
    }
}
