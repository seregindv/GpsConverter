﻿using GpsConverter.Converter;
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
        // N056 44.668, E037 11.621 дуб Музей ОИЯИ
        Regex _pointExpression = new Regex(@"[NS](\d+)\s+([\d\.]+)[\s,]+[EW](\d+)\s+([\d\.]+)\s+(.+)");
        protected override Regex PointExpression
        {
            get { return _pointExpression; }
        }

        protected override NamedEarthPoint CreatePoint(Match match)
        {
            return new NamedEarthPoint(match.Groups[3].Value, match.Groups[3].Value, "0", match.Groups[1].Value, match.Groups[2].Value, "0", match.Groups[5].Value.TrimEnd());
        }
    }
}
