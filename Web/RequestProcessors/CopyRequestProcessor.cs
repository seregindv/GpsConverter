﻿using GpsConverter.Parsers;
using System.Net;
using System.Windows.Forms;

namespace GpsConverter.Web.RequestProcessors
{
    public class CopyPointsRequestProcessor : WebRequestProcessor
    {
        public override string Prefix => "actions/copy";

        public override string Process(HttpListenerContext ctx)
        {
            Clipboard.SetText(JsonParser.ParsePoints(StreamToString(ctx.Request.InputStream)));
            return null;
        }
    }
}