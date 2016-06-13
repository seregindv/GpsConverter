using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using GpsConverter.Converter;
using Newtonsoft.Json.Linq;

namespace GpsConverter.Web.RequestProcessors
{
    public interface IWebRequestProcessor
    {
        string Prefix { get; }
        string Process(HttpListenerContext ctx);
    }

    public class MapRequestProcessor : IWebRequestProcessor
    {
        public string Prefix { get { return "map"; } }

        public string Process(HttpListenerContext ctx)
        {
            return File.ReadAllText("web\\yandex_maps.html");
        }
    }

    public class CopyRequestProcessor : IWebRequestProcessor
    {
        public string Prefix { get { return "actions/copy"; } }

        public string Process(HttpListenerContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }

    public class GetRequestProcessor : IWebRequestProcessor
    {
        private readonly IPoiProvider _poiProvider;

        public GetRequestProcessor(IPoiProvider poiProvider)
        {
            _poiProvider = poiProvider;
        }
        public string Prefix { get { return "actions/get"; } }

        public string Process(HttpListenerContext ctx)
        {
            var points = _poiProvider.GetPoints();
            var result = new JObject(
                from point in points
                select new JProperty(
                        Guid.NewGuid().ToString(),
                        new JArray(
                            new JArray(new JArray(point.Latitude, point.Longitude)),
                            point.Name)

                )
            );
            return result.ToString();
        }
    }
}
