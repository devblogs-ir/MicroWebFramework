using System;

namespace MicroWebFramework;
public class HttpContext
{
    public HttpContext(string ipAdrress, string requestedUrl)
    {
        Id = Guid.NewGuid();
        IpAddress = ipAdrress;
        Request = new HttpRequest()
        {
            Url = requestedUrl
        };
        Response = new();
    }
    public string IpAddress { get; init; }
    public Guid Id { get; init; }
    public HttpRequest Request { get; init; }
    public HttpResponse Response { get; init; }
}
