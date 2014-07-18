using GpsConverter.Converter;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsConverter.Wikimapia
{
    public class CultTourizmDescriptionParser : DescriptionParser
    {
        protected Regex _addressExpression = new Regex(@"http:\/\/(www\.)?culttourism\.ru");
        protected override Regex AddressExpression
        {
            get { return _addressExpression; }
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

        public override bool TryParse(string text)
        {
            var doc = LoadHtml(text);
            var navigator = doc.CreateNavigator();
            var points = navigator.SelectDescendants("tr", String.Empty, false)
                .OfType<HtmlNodeNavigator>()
                .Where(node =>
                {
                    var attr = node.GetAttribute("class", String.Empty);
                    if (String.IsNullOrEmpty(attr))
                        return false;
                    return attr.StartsWith("obj_type_");
                })
                .Select(node =>
                    new
                    {
                        Description = node.SelectSingleNode(@"td[not(@class)]/a[@class='objlink']/strong"),
                        Coordinates = node.SelectSingleNode(@"td[@class='obj_additional']/span[@class='point_gpsshort']")
                    })
                 .Where(point => point.Description != null && point.Coordinates != null)
                 .Select(point => new ClipboardPoint
                 {
                     Coordinates = point.Coordinates.Value,
                     Description = point.Description.Value
                 }).ToArray();
            Points = points;
            return points.Length > 0;
        }
    }
}
