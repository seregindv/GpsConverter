using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsConverter.Wikimapia
{
    public class NewWikimapiaDescriptionParser : RegexDescriptionParser
    {
        readonly Regex _addressExpression = new Regex(@"http:\/\/(www\.)?wikimapia\.org\/\d+\/\w+\/.*");
        protected override Regex AddressExpression
        {
            get { return _addressExpression; }
        }

        readonly Regex _textExpression = new Regex(@"<h1\s+itemprop=""name"".+?>(.+?)<.+Координаты:.+?<\/span>(.+?)<", RegexOptions.Singleline);
        protected override Regex TextExpression
        {
            get { return _textExpression; }
        }

        protected override IEnumerable<ClipboardPoint> GetPoints(MatchCollection matches)
        {
            yield return new ClipboardPoint
            {
                Description = matches[0].Groups[1].Value,
                Coordinates = matches[0].Groups[2].Value
            };
        }
    }
}
