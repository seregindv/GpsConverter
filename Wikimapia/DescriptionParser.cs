using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsConverter.Wikimapia
{
    public abstract class DescriptionParser
    {
        public IEnumerable<ClipboardPoint> Points { protected set; get; }

        protected abstract Regex AddressExpression { get; }

        public bool CheckAddress(string address)
        {
            return AddressExpression.Match(address).Success;
        }

        public abstract bool TryParse(string text);

        static DescriptionParser[] _parsers =
        {
            new OldWikimapiaDescriptionParser(),
            new NewWikimapiaDescriptionParser(),
            new CultTourizmDescriptionParser()
        };
        public static DescriptionParser Get(string address)
        {
            return _parsers.FirstOrDefault(parser => parser.CheckAddress(address));
        }
    }

}
