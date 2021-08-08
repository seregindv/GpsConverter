using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace GpsConverter.Web.RequestProcessors
{
    public class CopyPathRequestProcessor : WebRequestProcessor
    {
        public override string Prefix => "actions/copy-path";

        public override string Process(HttpListenerContext ctx)
        {
            Clipboard.SetText(StreamToString(ctx.Request.InputStream));
            return null;
        }
    }
}
