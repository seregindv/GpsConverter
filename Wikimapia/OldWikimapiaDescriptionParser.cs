using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsConverter.Wikimapia
{
    public class OldWikimapiaDescriptionParser : RegexDescriptionParser
    {
        readonly Regex _addressExpression = new Regex(@"http:\/\/(www\.)?old\.wikimapia\.org\/\d+\/\w+\/.*");
        protected override Regex AddressExpression
        {
            get { return _addressExpression; }
        }

        readonly Regex _textExpression = new Regex(@">(?:Координаты|Coordinates):\s*(.+?)<.+<h1 style=""font-size:\s*20px; display:\s*inline;"">(.+?)<", RegexOptions.Singleline);
        protected override Regex TextExpression
        {
            get { return _textExpression; }
        }

        protected override IEnumerable<ClipboardPoint> GetPoints(MatchCollection matches)
        {
            yield return new ClipboardPoint
            {
                Coordinates = matches[0].Groups[1].Value,
                Description = matches[0].Groups[2].Value
            };
        }
    }
}
