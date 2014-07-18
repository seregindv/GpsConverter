using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace GpsConverter.Converter
{
    public class NamedEarthPoint : EarthPoint
    {
        protected string _name;

        public NamedEarthPoint(double longitude, double latitude, string name) :
            base(longitude, latitude)
        {
            Name = name;
        }

        public NamedEarthPoint(string lonDeg, string lonMin, string lonSec,
            string latDeg, string latMin, string latSec, string name) :
            this(ToDeg(Double.Parse(lonDeg, CultureInfo.InvariantCulture), Double.Parse(lonMin, CultureInfo.InvariantCulture), Double.Parse(lonSec, CultureInfo.InvariantCulture)),
                ToDeg(Double.Parse(latDeg, CultureInfo.InvariantCulture), Double.Parse(latMin, CultureInfo.InvariantCulture), Double.Parse(latSec, CultureInfo.InvariantCulture)), name)
        {
        }

        private static double ToDeg(double deg, double min, double sec)
        {
            return deg + min / 60 + sec / 3600;
        }

        public string Name
        {
            set { _name = value; }
            get { return _name.TrimEnd(' ', '\n', '\r', '\t'); }
        }
    }
}
