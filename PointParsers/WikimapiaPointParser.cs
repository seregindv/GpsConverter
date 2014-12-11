using GpsConverter.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsConverter.PointParsers
{
    public class WikimapiaPointParser : RegexPointPaser
    {
        private Regex _pointExpression = new Regex(@"[^\d]*(?<lat_deg>\d+).(?<lat_min>\d+).(?<lat_sec>\d+).(?<lat_dir>[NS])\s+(?<lon_deg>\d+).(?<lon_min>\d+).(?<lon_sec>\d+).(?<lon_dir>[WE])\s+(?<name>.+)");

        protected override Regex PointExpression
        {
            get { return _pointExpression; }
        }

        protected override NamedEarthPoint CreatePoint(Match match)
        {
            return new NamedEarthPoint(
                   match.Groups["lon_deg"].Value, match.Groups["lon_min"].Value, match.Groups["lon_sec"].Value,
                   match.Groups["lat_deg"].Value, match.Groups["lat_min"].Value, match.Groups["lat_sec"].Value,
                   match.Groups["name"].Value);
        }

        public override string FormatSample
        {
            get { return @"55°52'48""N   38°46'38""E MTB Skill Park"; }
        }
    }
}
