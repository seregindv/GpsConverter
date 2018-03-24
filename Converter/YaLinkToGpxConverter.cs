using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;

namespace GpsConverter.Converter
{
    class YaLinkToGpxConverter : LinkConverterBase
    {
        private const string linkStruct = @".*?rl=(?<ini_lon>[\d.]+)%2C(?<ini_lat>[\d.]+)(~(?<lon_off>[\d\.-]+)%2C(?<lat_off>[\d\.-]+))*";

        #region IEarthConverter Members

        public override IList<NamedEarthPoint> GetPoints(string points)
        {
            var result = new List<NamedEarthPoint>();
            var match = Regex.Match(points, linkStruct);
            if (!match.Success)
                throw new ArgumentException("Path not found");

            var currentPoint = new EarthPoint(match.Groups["ini_lon"].Value, match.Groups["ini_lat"].Value);
            var counter = 1;
            result.Add(new NamedEarthPoint(currentPoint.Longitude, currentPoint.Latitude, "1"));

            var lonEnumerator = match.Groups["lon_off"].Captures.GetEnumerator();
            var latEnumerator = match.Groups["lat_off"].Captures.GetEnumerator();
            lonEnumerator.Reset();
            latEnumerator.Reset();
            do
            {
                if (!latEnumerator.MoveNext())
                {
                    if (lonEnumerator.MoveNext())
                        throw new Exception("Lontitude without latitude");
                    break;
                }
                if (!lonEnumerator.MoveNext())
                    throw new Exception("Latitude without lontitude");
                currentPoint.Offset(((Capture)lonEnumerator.Current).Value, ((Capture)latEnumerator.Current).Value);
                counter++;
                result.Add(new NamedEarthPoint(currentPoint.Longitude, currentPoint.Latitude, counter.ToString()));
            } while (true);
            return result;
        }

        #endregion
    }
}
