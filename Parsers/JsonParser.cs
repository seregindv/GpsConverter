using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GpsConverter.Parsers
{
    public static class JsonParser
    {
        public static string ParsePoints(string json)
        {
            var obj = JObject.Parse(json);
            var result = new StringBuilder();
            foreach (var point in obj.Children())
                foreach (var pointDataItem in point.Children())
                {
                    var lat = pointDataItem.First.First.Value<string>();
                    var lon = pointDataItem.First.Last.Value<string>();
                    var name = pointDataItem.Last.Value<string>();
                    result.AppendLine(string.Concat(lat, " ", lon, " ", name));
                }

            return result.ToString();
        }
    }
}
