using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace GpsConverter.Converter
{
    public class EarthPoint
    {
        public EarthPoint(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public EarthPoint(string longitude, string latitude)
            : this(Double.Parse(longitude, CultureInfo.InvariantCulture), Double.Parse(latitude, CultureInfo.InvariantCulture))
        {
        }

        public double Longitude;
        public double Latitude;

        public string StringLongitude
        {
            get { return Longitude.ToString(CultureInfo.InvariantCulture); }
        }

        public string StringLatitude
        {
            get { return Latitude.ToString(CultureInfo.InvariantCulture); }
        }

        public void Offset(double longitude, double latitude)
        {
            Longitude += longitude;
            Latitude += latitude;
        }

        public void Offset(string longitude, string latitude)
        {
            Offset(Double.Parse(longitude, CultureInfo.InvariantCulture), Double.Parse(latitude, CultureInfo.InvariantCulture));
        }
    }
}
