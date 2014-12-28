using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace GpsConverter.Converter
{
    class GpxConverter : IPoiConverter
    {
        public IList<NamedEarthPoint> GetPoints(string points)
        {
            return XDocument.Load(new StringReader(points)).XPathSelectElements("/gpx/wpt").Select(element =>
                new NamedEarthPoint(
                    Double.Parse(element.Attribute("lon").Value, CultureInfo.InvariantCulture),
                    Double.Parse(element.Attribute("lat").Value, CultureInfo.InvariantCulture),
                    element.Element("name").Value)).ToArray();
        }
    }
}
