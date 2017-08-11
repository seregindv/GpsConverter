using System.Net;
using System.Security.Policy;

namespace GpsConverter.Web.RequestProcessors
{
    public interface IWebRequestProcessor
    {
        string Prefix { get; }
        string Process(HttpListenerContext ctx);
    }
}
