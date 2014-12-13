﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GpsConverter.Converter;

namespace GpsConverter.PointParsers
{
    public class ManualPointParser : RegexPointPaser
    {
        readonly Regex _pointExpression = new Regex(@"(\-?[\d,\.]+)\s+(\-?[\d,\.]+)\s+(.+)");

        protected override Regex PointExpression
        {
            get { return _pointExpression; }
        }

        protected override NamedEarthPoint CreatePoint(Match match)
        {
            double lat, lon;
            if (!Double.TryParse(match.Groups[1].Value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out lat)
             || !Double.TryParse(match.Groups[2].Value.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out lon))
                return null;
            return new NamedEarthPoint(lon, lat, match.Groups[3].Value);
        }

        public override string FormatSample
        {
            get { return @"55.87719 38.78353 ул. Советская"; }
        }
    }
}
