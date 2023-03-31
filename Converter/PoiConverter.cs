using GpsConverter.Helpers;
using GpsConverter.PointParsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace GpsConverter.Converter
{
    public class PoiConverter : IEarthConverter
    {
        private readonly IPoiConverter _converter;

        public PoiConverter(IPoiConverter converter)
        {
            _converter = converter;
        }

        #region IEarthConverter Members

        public virtual IList<NamedEarthPoint> GetPoints(string points)
        {
            return _converter.GetPoints(points);
        }

        public virtual ConvertResult[] Convert(string something)
        {
            var points = GetPoints(something);
            var result = new ConvertResult[3];
            result[0] = new ConvertResult("GPX", GetGpx(points));
            result[1] = new ConvertResult("KML", GetKml(points));
            result[2] = new ConvertResult("LMX", GetLmx(points)) { IsOutdated = true };
            return result;
        }

        public string Name { get; set; }

        #endregion

        private string GetGpx(IList<NamedEarthPoint> points)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XElement("gpx", points.Select(point =>
                {
                    var hasDescription = !string.IsNullOrEmpty(point.Description);
                    var content = new XObject[hasDescription ? 4 : 3];
                    content[0] = new XAttribute("lat", point.StringLatitude);
                    content[1] = new XAttribute("lon", point.StringLongitude);
                    content[2] = new XElement("name", point.Name);
                    if (hasDescription)
                        content[3] = new XElement("desc", point.Description);
                    return new XElement("wpt", content);
                })));
            return doc.AsString();
        }

        private string GetLmx(IList<NamedEarthPoint> points)
        {
            StringBuilder result = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.Encoding = Encoding.UTF8;
            using (XmlWriter xmlResult = XmlWriter.Create(result, settings))
            {
                string ns = "http://www.nokia.com/schemas/location/landmarks/1/0";
                string prefix = "lm";
                xmlResult.WriteStartElement("lm", "lmx", ns);
                xmlResult.WriteStartElement(prefix, "landmarkCollection", ns);
                xmlResult.WriteElementString(prefix, "name", ns, "Landmarks");
                foreach (NamedEarthPoint point in points)
                {
                    xmlResult.WriteStartElement(prefix, "landmark", ns);
                    xmlResult.WriteElementString(prefix, "name", ns, point.Name);
                    if (!string.IsNullOrEmpty(point.Description))
                        xmlResult.WriteElementString(prefix, "description", ns, point.Description);
                    xmlResult.WriteStartElement(prefix, "coordinates", ns);
                    xmlResult.WriteElementString(prefix, "latitude", ns, point.StringLatitude);
                    xmlResult.WriteElementString(prefix, "longitude", ns, point.StringLongitude);
                    xmlResult.WriteEndElement(); // lm:coordinates
                    xmlResult.WriteEndElement(); // lm:landmark
                }
                xmlResult.WriteEndElement(); // lm:landmarkCollection
                xmlResult.WriteEndElement(); // lm:lmx
            }
            return result.ToString();
        }

        private string GetKml(IList<NamedEarthPoint> points)
        {
            var ns = XNamespace.Get("http://www.opengis.net/kml/2.2");
            var doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XElement(ns + "kml",
                    new XElement(ns + "Document",
                        new XElement(ns + "name", Name),
                        points.Select(point =>
                        {
                            var hasDescription = !string.IsNullOrEmpty(point.Description);
                            var content = new XObject[hasDescription ? 3 : 2];
                            content[0] = new XElement(ns + "name", point.Name);
                            content[1] = new XElement(ns + "Point",
                                new XElement(ns + "coordinates",
                                    point.StringLongitude + "," + point.StringLatitude
                                )
                            );
                            if (hasDescription)
                                content[2] = new XElement(ns + "description", point.Description);
                            return new XElement(ns + "Placemark", content);

                        })
                    )
                )
            ); ;
            return doc.AsString();
        }
    }
}
