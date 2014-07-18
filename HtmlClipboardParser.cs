using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GpsConverter
{
    public class HtmlClipboardParser
    {
        public string SourceUrl { set; get; }
        public string HtmlText { set; get; }
        static readonly Regex sourceUrlRegex = new Regex(@"^SourceURL\s*:\s*(.+)$");
        static readonly Regex htmlStartRegex = new Regex(@"^\s*<");

        public virtual bool Parse()
        {
            if (!Clipboard.ContainsData("HTML Format"))
                return false;
            SourceUrl = null;
            HtmlText = null;
            using (var reader = new StringReader((string)Clipboard.GetData("HTML Format")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var match = sourceUrlRegex.Match(line);
                    if (SourceUrl == null && match.Success)
                    {
                        SourceUrl = match.Groups[1].Value;
                        continue;
                    }
                    if (SourceUrl != null)
                    {
                        match = htmlStartRegex.Match(line);
                        if (match.Success)
                        {
                            HtmlText = line + Environment.NewLine + reader.ReadToEnd();
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
