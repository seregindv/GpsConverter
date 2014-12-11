using GpsConverter.Converter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsConverter.PointParsers
{
    public class CultTourizmPointParser : RegexPointPaser
    {
        Regex _pointExpression = new Regex(@"[NS]([\d\.]+)\s+[EW]([\d\.]+)\s+(.+)");
        protected override Regex PointExpression
        {
            get { return _pointExpression; }
        }

        protected override NamedEarthPoint CreatePoint(Match match)
        {
            return new NamedEarthPoint(Double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture), Double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture), match.Groups[3].Value.TrimEnd());
        }

        public override string FormatSample
        {
            get { return @"N57.44706 E42.15913  Дом купца И. Шемякина"; }
        }
    }
}
