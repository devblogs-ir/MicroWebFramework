using System.Net;

namespace MicroWebFramework.Models;
public class HttpContext
{
    public required string IP { get; set; }

    public string Url { get; set; }

    public HttpListenerResponse Response { get; set; }
    public HttpListenerRequest Request { get; set; }
}