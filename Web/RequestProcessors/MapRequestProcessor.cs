using System.IO;
using System.Net;

namespace GpsConverter.Web.RequestProcessors
{
    public class MapRequestProcessor : WebRequestProcessor
    {
        public override string Prefix => "map";

        public override string Process(HttpListenerContext ctx)
        {
            Out(ctx.Request);
            return File.ReadAllText("web\\yandex_maps.html");
        }

        private void Out(HttpListenerRequest request)
        {
            Out("==============");
            if (request.AcceptTypes != null)
            {
                Out("AcceptTypes ->");
                foreach (var acceptType in request.AcceptTypes)
                    Out($"-> {acceptType}");
            }
            Out($"ContentEncoding: {request.ContentEncoding.WebName}");
            Out($"ContentType: {request.ContentType}");
            Out($"HttpMethod: {request.HttpMethod}");
            Out("Cookies ->");
            foreach (Cookie cookie in request.Cookies)
                Out($"-> {cookie.Name} = {cookie.Value}");
            Out("Headers ->");
            for (var i = 0; i < request.Headers.Count; i++)
                Out($"-> {request.Headers.Keys[i]} = {request.Headers[i]}");
            Out($"UserAgent: {request.UserAgent}");
            Out("QueryString ->");
            for (var i = 0; i < request.QueryString.Count; i++)
                Out($"-> {request.QueryString.Keys[i]} = {request.QueryString[i]}");
            Out($"ProtocolVersion: {request.ProtocolVersion}");
            Out($"ServiceName: {request.ServiceName}");
            Out($"Url: {request.Url}");
            Out($"RawUrl: {request.RawUrl}");
            Out($"UrlReferrer: {request.UrlReferrer}");
        }

        private void Out(string line)
        {
            System.Diagnostics.Debug.WriteLine(line);
        }
    }
}