using System.Net;

namespace MicroWebFrameworkConsole
{
    public class HTTPContext
    {
        public HttpListenerRequest Request { get; set; }
        public HttpListenerResponse Response { get; set; }
        public string IP { get; set; }
        public string Url { get; set; }
    }
}
