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
        protected string _description;

        public NamedEarthPoint(double longitude, double latitude, string name, string description) :
            base(longitude, latitude)
        {
            Name = name;
            Description = description;
        }

        public NamedEarthPoint(string lonDeg, string lonMin, string lonSec,
            string latDeg, string latMin, string latSec, string name, string description) :
            this(ToDeg(Double.Parse(lonDeg, CultureInfo.InvariantCulture), Double.Parse(lonMin, CultureInfo.InvariantCulture), Double.Parse(lonSec, CultureInfo.InvariantCulture)),
                ToDeg(Double.Parse(latDeg, CultureInfo.InvariantCulture), Double.Parse(latMin, CultureInfo.InvariantCulture), Double.Parse(latSec, CultureInfo.InvariantCulture)), name, description)
        {
        }

        private static double ToDeg(double deg, double min, double sec)
        {
            return deg + min / 60 + sec / 3600;
        }

        public string Description
        {
            set => _description = Trim(value);
            get => _description;
        }

        public string Name
        {
            set => _name = Trim(value);
            get => _name;
        }

        private string Trim(string val)
        {
            return val?.TrimEnd(' ', '\n', '\r', '\t');
        }
    }
}
