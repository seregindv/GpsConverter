using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace GpsConverter.Web.Servers
{
    public class WebServer
    {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerContext, string> _responderMethod;

        public WebServer(IEnumerable<string> prefixes, Func<HttpListenerContext, string> method)
        {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException(
                    "Needs Windows XP SP2, Server 2003 or later.");

            // URI prefixes are required, for example 
            // "http://localhost:8080/index/".
            if (prefixes == null)
                throw new ArgumentNullException("prefixes");

            // A responder method is required
            if (method == null)
                throw new ArgumentNullException("method");

            foreach (string s in prefixes)
                _listener.Prefixes.Add(s);

            _responderMethod = method;
            _listener.Start();
        }

        public WebServer(Func<HttpListenerContext, string> method, params string[] prefixes)
            : this(prefixes, method) { }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Console.WriteLine("Webserver running...");
                try
                {
                    while (_listener.IsListening)
                    {
                        var subThread = new Thread(c =>
                        {
                            var ctx = c as HttpListenerContext;
                            if (ctx == null)
                                return;
                            try
                            {
                                string rstr = _responderMethod(ctx);
                                byte[] buf = Encoding.UTF8.GetBytes(rstr ?? String.Empty);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            catch { } // suppress any exceptions
                            finally
                            {
                                // always close the stream
                                ctx.Response.OutputStream.Close();
                            }
                        });
                        subThread.SetApartmentState(ApartmentState.STA);
                        subThread.Start(_listener.GetContext());
                    }
                }
                catch { } // suppress any exceptions
            });
        }

        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}
