using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace GpsConverter.Converter
{
    public class KmlConverter : IPoiConverter
    {
        public IList<NamedEarthPoint> GetPoints(string points)
        {
            var doc = XDocument.Load(new StringReader(points));
            var firstElement = doc.Elements().First();
            var ns = firstElement.GetDefaultNamespace();
            var nsManager = new XmlNamespaceManager(new NameTable());
            nsManager.AddNamespace("kml", ns.NamespaceName);
            return firstElement.XPathSelectElements("//kml:Placemark", nsManager)
                .Select(element =>
                {
                    var coordinates = element
                        .XPathSelectElement("kml:Point/kml:coordinates", nsManager)
                        .Value
                        .Split(',')
                        .Select(c => Double.Parse(c, CultureInfo.InvariantCulture))
                        .ToArray();
                    return new NamedEarthPoint(
                        coordinates[0],
                        coordinates[1],
                        element.Element(ns + "name").Value,
                        element.Element(ns + "description")?.Value);
                }).ToArray();

        }
    }
}
