using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsConverter.Wikimapia
{
    public abstract class RegexDescriptionParser : DescriptionParser
    {
        protected abstract Regex TextExpression { get; }

        protected abstract IEnumerable<ClipboardPoint> GetPoints(MatchCollection matches);

        public override bool TryParse(string text)
        {
            var textMatches = TextExpression.Matches(text);
            if (textMatches.Count == 0)
                return false;
            Points = GetPoints(textMatches);
            return true;
        }
    }
}
