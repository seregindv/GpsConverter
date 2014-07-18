using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace GpsConverter.Converter
{
    // http://www.free-wifi.cz/big_map.php
    public class CzWiFiMapConverter : IPoiConverter
    {
        public IList<NamedEarthPoint> GetPoints(string points)
        {
            var regex = new Regex(@"GLatLng\s*\(\s*([\d\.]+)\s*\,\s*([\d\.]+).+?\>(.+?)\<\/a\>\<br\s*\/\>(.+?)""", RegexOptions.Singleline);
            return regex.Matches(points).Cast<Match>()
                .Select(
                    match =>
                        new NamedEarthPoint(Double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture),
                            Double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture), match.Groups[3].Value + " [" + match.Groups[4].Value + "]")).ToArray();
        }
    }
}