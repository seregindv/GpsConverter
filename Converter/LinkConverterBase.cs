using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GpsConverter.Converter
{
    public abstract class LinkConverterBase : IEarthConverter
    {
        public string Name { get; set; }

        public ConvertResult[] Convert(string something)
        {
            var points = GetPoints(something);
            var result = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true
            };
            using (var xmlResult = XmlWriter.Create(result, settings))
            {
                xmlResult.WriteStartElement("gpx");
                xmlResult.WriteStartElement("trk");
                xmlResult.WriteStartElement("trkseg");
                foreach (var point in points)
                    WriteTrkpt(xmlResult, point);
                xmlResult.WriteEndElement(); // trkseg
                xmlResult.WriteEndElement(); // trk
                xmlResult.WriteEndElement(); // gpx
                xmlResult.Close();
            }
            return new[] { new ConvertResult("GPX", result.ToString()) };
        }

        public abstract IList<NamedEarthPoint> GetPoints(string points);

        private void WriteTrkpt(XmlWriter writer, EarthPoint point)
        {
            writer.WriteStartElement("trkpt");
            writer.WriteAttributeString("lon", point.StringLongitude);
            writer.WriteAttributeString("lat", point.StringLatitude);
            writer.WriteEndElement();
        }

    }
}