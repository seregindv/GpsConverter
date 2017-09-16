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
            result[0] = new ConvertResult("GPX", GetWpt(points));
            result[1] = new ConvertResult("LMX", GetLmx(points));
            result[2] = new ConvertResult("KML", GetKml(points));
            return result;
        }

        public string Name { get; set; }

        #endregion

        private string GetWpt(IList<NamedEarthPoint> points)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XElement("gpx",
                    from point in points
                    select new XElement("wpt",
                        new XAttribute("lat", point.StringLatitude),
                        new XAttribute("lon", point.StringLongitude),
                        new XElement("name", point.Name)
                    )
                )
            );
            return doc.GetString();
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
                        from point in points
                        select new XElement(ns + "Placemark",
                            new XElement(ns + "name", point.Name),
                            new XElement(ns + "Point",
                                new XElement(ns + "coordinates",
                                    point.StringLongitude + "," + point.StringLatitude
                                )
                            )
                        )
                    )
                )
            );
            return doc.GetString();
        }
    }
}
