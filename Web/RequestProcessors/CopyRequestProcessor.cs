using System.Net;
using System.Windows.Forms;

namespace GpsConverter.Web.RequestProcessors
{
    public class CopyRequestProcessor : WebRequestProcessor
    {
        public override string Prefix => "actions/copy";

        public override string Process(HttpListenerContext ctx)
        {
            Clipboard.SetText(JsonParser.ParsePoints(StreamToString(ctx.Request.InputStream)));
            return null;
        }
    }
}