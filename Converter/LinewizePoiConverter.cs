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
    public class LinewizePoiConverter : PoiConverter
    {
        public LinewizePoiConverter()
            : base(null)
        {
            UnparsedLines = new List<string>();
        }

        #region IEarthConverter Members

        private IList<string> UnparsedLines { set; get; }

        public override IList<NamedEarthPoint> GetPoints(string points)
        {
            UnparsedLines.Clear();
            var result = new List<NamedEarthPoint>();
            using (var reader = new StringReader(points))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var point = PointParser.Parse(line);
                    if (point != null)
                        result.Add(point);
                    else
                        UnparsedLines.Add(line);
                }
            }
            return result;
        }

        public override ConvertResult[] Convert(string something)
        {
            var result = base.Convert(something);
            if (UnparsedLines.Count > 0)
            {
                var unparsed = UnparsedLines.Aggregate(new StringBuilder(),
                    (builder, line) => builder.AppendLine(line),
                    builder => builder.ToString());
                result = result.Concat(new[] { new ConvertResult("Unparsed", unparsed, true) }).ToArray();
            }
            return result;
        }

        #endregion
    }
}
