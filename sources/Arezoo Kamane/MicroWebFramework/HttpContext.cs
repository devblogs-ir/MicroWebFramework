using System.Net;

namespace MicroWebFramework;

public class HttpContext
{
    public HttpListenerRequest Request { get; set; }

    public HttpListenerResponse Response { get; set; }

}
