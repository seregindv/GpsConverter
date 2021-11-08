using GpsConverter.Converter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsConverter.PointParsers
{
    public class AutotravelPointParser : RegexPointPaser
    {
        protected override Regex PointExpression { get; } = new Regex(@"[NS]\s*(\d+)\D+([\d\.]+)\D?[\s,]+[EW]\s*(\d+)\D+([\d\.]+)\D?[\s]+(.+?)(?:(?:\|\|\|)(.+))?$");

        protected override NamedEarthPoint CreatePoint(Match match)
        {
            return new NamedEarthPoint(match.Groups[3].Value, match.Groups[4].Value, "0",
                match.Groups[1].Value, match.Groups[2].Value, "0", 
                match.Groups[5].Value.TrimEnd(), match.Groups[6].Value?.TrimEnd());
        }

        public override string FormatSample => @"N056 44.668, E037 11.621 Музей ОИЯИ";
    }
}
