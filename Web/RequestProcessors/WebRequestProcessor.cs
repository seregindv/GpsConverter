using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GpsConverter.Web.RequestProcessors
{
    public abstract class WebRequestProcessor : IWebRequestProcessor
    {
        public abstract string Prefix { get; }
        public abstract string Process(HttpListenerContext ctx);

        public string StreamToString(Stream stream)
        {
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}
