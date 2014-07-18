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

        #region static

        static readonly PointParser[] _parsers =
        { 
            new CultTourizmPointParser(), 
            new WikimapiaPointParser(),
            new AutotravelPointParser()
        };

        public static NamedEarthPoint Parse(string point)
        {
            var parser = _parsers.FirstOrDefault(p => p.TryParse(point));
            if (parser == null)
                return null;
            return parser.Point;
        }

        #endregion
    }
}
