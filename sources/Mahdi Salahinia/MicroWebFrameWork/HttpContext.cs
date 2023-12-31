using System.Net;

namespace MicroWebFrameWork;

public class HttpContext
{
    public required string IP { get; set; }
    public required string Url { get; set; }
    public HttpListenerResponse Response { get; set; }
    public HttpListenerRequest Request { get; set; }
}
