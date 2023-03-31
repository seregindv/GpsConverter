using GpsConverter.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GpsConverter.Wikimapia
{
    public class ClipboardCoordinatesParser : HtmlClipboardParser
    {
        public IEnumerable<ClipboardPoint> Points { private set; get; }

        public override bool Parse()
        {
            if (!base.Parse())
                return false;
            var parser = DescriptionParser.Get(SourceUrl);
            if (parser == null)
                return false;
            HtmlText = Decode(HtmlText, Encoding.Default, Encoding.UTF8);
            if (!parser.TryParse(HtmlText))
                return false;
            Points = parser.Points;
            return true;
        }

        string Decode(string strHtml, Encoding fromEncoding, Encoding toEncoding)
        {
            var bytes = fromEncoding.GetBytes(strHtml);
            var toChars = new char[toEncoding.GetCharCount(bytes, 0, bytes.Length)];
            toEncoding.GetChars(bytes, 0, bytes.Length, toChars, 0);
            return new string(toChars);
        }
    }
}
