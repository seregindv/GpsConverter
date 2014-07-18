using GpsConverter.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsConverter.PointParsers
{
    public abstract class RegexPointPaser : PointParser
    {
        protected abstract Regex PointExpression { get; }

        protected abstract NamedEarthPoint CreatePoint(Match match);

        public override bool TryParse(string point)
        {
            var match = PointExpression.Match(point);
            if (!match.Success)
                return false;
            Point = CreatePoint(match);
            return true;
        }

    }
}
