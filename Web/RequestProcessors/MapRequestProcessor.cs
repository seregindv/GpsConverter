using System.IO;
using System.Net;

namespace GpsConverter.Web.RequestProcessors
{
    public class MapRequestProcessor : WebRequestProcessor
    {
        public override string Prefix => "map";

        public override string Process(HttpListenerContext ctx)
        {
            return File.ReadAllText("web\\yandex_maps.html");
        }
    }
}