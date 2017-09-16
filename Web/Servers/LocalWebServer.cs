using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using GpsConverter.Web.RequestProcessors;

namespace GpsConverter.Web.Servers
{
    public class LocalWebServer
    {
        private readonly WebServer _server;
        private readonly Uri _baseUri;
        private readonly Dictionary<string, IWebRequestProcessor> _processors;

        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            try
            {
                listener.Start();
                return ((IPEndPoint)listener.LocalEndpoint).Port;
            }
            finally
            {
                listener.Stop();
            }
        }

        public LocalWebServer(params IWebRequestProcessor[] processors)
        {
            _processors = processors.ToDictionary(processor => processor.Prefix);
            var port = GetRandomUnusedPort();
            _baseUri = new Uri("http://localhost:" + port);
            _server = new WebServer(_processors.Select(processor => GetAddress(processor.Value.Prefix)).ToArray(), ProcessRequest);
        }
        private string GetAddress(string arg)
        {
            var result = new Uri(_baseUri, arg).ToString();
            if (!result.EndsWith("/", StringComparison.Ordinal))
                result += "/";
            return result;
        }

        private string ProcessRequest(HttpListenerContext ctx)
        {
            IWebRequestProcessor processor;
            if (_processors.TryGetValue(ctx.Request.Url.LocalPath.Trim('/'), out processor))
                return processor.Process(ctx);
            
            ctx.Response.StatusCode = 404;
            return null;
        }

        public string StreamToString(Stream stream)
        {
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        public void Start()
        {
            _server.Run();
        }

        public void Stop()
        {
            _server.Stop();
        }

        public Uri BaseUri => _baseUri;
    }
}
