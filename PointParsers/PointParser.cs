using System.Diagnostics;
using GpsConverter.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpsConverter.PointParsers
{
    public abstract class PointParser
    {
        public NamedEarthPoint Point { protected set; get; }

        public abstract bool TryParse(string point);

        public abstract string FormatSample { get; }

        #region static

        static readonly PointParser[] _parsers =
        { 
            new WikimapiaPointParser(),
            new ManualPointParser(),
            new CultTourizmPointParser(),
            new AutotravelPointParser()
        };

        public static NamedEarthPoint Parse(string point)
        {
            var parser = _parsers.FirstOrDefault(p => p.TryParse(point));
            if (parser == null)
                return null;
            return parser.Point;
        }

        public static IEnumerable<string> GetFormatSamples()
        {
            return _parsers.Select(parser => parser.FormatSample);
        }

        #endregion
    }
}
