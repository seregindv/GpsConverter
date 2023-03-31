using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GpsConverter.Helpers
{
    public static class XExtensions
    {
        public static string AsString(this XDocument doc)
        {
            var encoding = doc.Declaration != null && doc.Declaration.Encoding != null
                ? Encoding.GetEncoding(doc.Declaration.Encoding)
                : Encoding.Default;
            using (var memStream = new MemoryStream())
            using (var sw = new StreamWriter(memStream, encoding))
            {
                doc.Save(sw);
                memStream.Position = 0L;
                using (var sr = new StreamReader(memStream))
                    return sr.ReadToEnd();
            }
        }
    }
}
