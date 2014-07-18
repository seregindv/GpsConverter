using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace GpsConverter.Converter
{
    // 
    public class KarlovyVaryBusMapConverter : IPoiConverter
    {
        public IList<NamedEarthPoint> GetPoints(string points)
        {
            var doc = LoadHtml(points);
            return doc.DocumentNode
                .SelectNodes("//div[@data-jmapping]")
                .Select(node =>
                {
                    var match = Regex.Match(node.GetAttributeValue("data-jmapping", String.Empty), @"lng:\s*([\d.]+),\s*lat:\s*([\d.]+)", RegexOptions.IgnoreCase);
                    if (!match.Success)
                        return null;
                    var stopNameNode = node.SelectSingleNode("div/p[1]");
                    var stopName = stopNameNode == null ? String.Empty : stopNameNode.InnerText;
                    var busNumbers = String.Join(", ", node.SelectNodes("div/p[2]/a").Select(n => n.InnerText).ToArray());
                    return new NamedEarthPoint(Double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture), Double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture), stopName + " [" + busNumbers + "]");
                }).ToArray();
        }

        private HtmlDocument LoadHtml(string html)
        {
            var result = new HtmlDocument
            {
                OptionFixNestedTags = true
            };
            result.LoadHtml(html);
            return result;
        }
    }
}
