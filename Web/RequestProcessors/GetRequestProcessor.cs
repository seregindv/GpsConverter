using System;
using System.Linq;
using System.Net;
using GpsConverter.Converter;
using Newtonsoft.Json.Linq;

namespace GpsConverter.Web.RequestProcessors
{
    public class GetRequestProcessor : WebRequestProcessor
    {
        private readonly IPoiProvider _poiProvider;

        public GetRequestProcessor(IPoiProvider poiProvider)
        {
            _poiProvider = poiProvider;
        }
        public override string Prefix => "actions/get";

        public override string Process(HttpListenerContext ctx)
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
