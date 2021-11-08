using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsConverter.Converter
{
    public class DoubleGisToGpxConverter : LinkConverterBase
    {
        public override IList<NamedEarthPoint> GetPoints(string points)
        {
            var result = new List<NamedEarthPoint>();
            var match = Regex.Match(points, @".+points%2F((?<lon>\d+\.+\d+)%20(?<lat>\d+\.+\d+)(?:%2C)?)*");
            if (!match.Success)
                throw new ArgumentException("Path not found");
            var lonEnumerator = match.Groups["lon"].Captures.GetEnumerator();
            var latEnumerator = match.Groups["lat"].Captures.GetEnumerator();
            lonEnumerator.Reset();
            latEnumerator.Reset();
            var counter = 0;
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
                counter++;
                result.Add(new NamedEarthPoint(
                    Double.Parse(((Capture)lonEnumerator.Current).Value, CultureInfo.InvariantCulture),
                    Double.Parse(((Capture)latEnumerator.Current).Value, CultureInfo.InvariantCulture),
                    counter.ToString(), null));
            } while (true);
            return result;
        }
    }
}