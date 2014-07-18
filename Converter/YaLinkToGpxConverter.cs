using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;

namespace GpsConverter.Converter
{
    class YaLinkToGpxConverter : IEarthConverter
    {
        private const string linkStruct = @".*?rl=(?<ini_lon>[\d.]+)%2C(?<ini_lat>[\d.]+)(~(?<lon_off>[\d\.-]+)%2C(?<lat_off>[\d\.-]+))*";

        private void WriteTrkpt(XmlWriter writer, EarthPoint point)
        {
            writer.WriteStartElement("trkpt");
            writer.WriteAttributeString("lon", point.StringLongitude);
            writer.WriteAttributeString("lat", point.StringLatitude);
            writer.WriteEndElement();
        }

        #region IEarthConverter Members

        public string[] Convert(string something)
        {
            Match match = Regex.Match(something, linkStruct);
            if (!match.Success)
                throw new ArgumentException("Path not found");
            StringBuilder result = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter xmlResult = XmlWriter.Create(result, settings))
            {
                xmlResult.WriteStartElement("gpx");
                xmlResult.WriteStartElement("trk");
                xmlResult.WriteStartElement("trkseg");
                EarthPoint point = new EarthPoint(match.Groups["ini_lon"].Value, match.Groups["ini_lat"].Value);
                WriteTrkpt(xmlResult, point);
                IEnumerator lonEnumerator = match.Groups["lon_off"].Captures.GetEnumerator();
                IEnumerator latEnumerator = match.Groups["lat_off"].Captures.GetEnumerator();
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
                    point.Offset(((Capture)lonEnumerator.Current).Value, ((Capture)latEnumerator.Current).Value);
                    WriteTrkpt(xmlResult, point);

                } while (true);
                xmlResult.WriteEndElement(); // trkseg
                xmlResult.WriteEndElement(); // trk
                xmlResult.WriteEndElement(); // gpx
                xmlResult.Close();
            }
            return new[] { result.ToString() };
        }

        #endregion
    }
}
