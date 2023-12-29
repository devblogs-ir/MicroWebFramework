using System.Net;

namespace MicroWebFramework;

public class HttpContext
{
    public HttpListenerResponse? Response { get; set; }
    public HttpListenerRequest Request { get; set; }

}