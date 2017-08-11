using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GpsConverter
{
    public static class JsonParser
    {
        public static string ParsePoints(string streamToString)
        {
            var j = JObject.Parse(streamToString);
            var result = new StringBuilder();
            foreach (var point in j.Children())
                foreach (var pointDataItem in point.Children())
                {
                    var lat = pointDataItem.First.First.Value<string>();
                    var lon = pointDataItem.First.Last.Value<string>();
                    var name = pointDataItem.Last.Value<string>();
                    result.AppendLine(String.Concat(lat, " ", lon, " ", name));
                }

            return result.ToString();
        }
    }
}
