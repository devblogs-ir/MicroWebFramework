using Dumpify;
using System.Net;
using System.Text;

namespace MicroWebFramework;
public class HttpAdapter : IUiAdapter
{
    private readonly HttpListener _listener;
    private Dictionary<Guid, HttpListenerContext> _unresponsedRequests;

    public HttpAdapter(string url)
    {
        _listener = new HttpListener();
        if (!url.EndsWith('/'))
        {
            url += "/";
        }
        _listener.Prefixes.Add(url);
        _listener.Start();
        $"HTTP Server is Listening at {url}".Dump();
        _unresponsedRequests = [];
    }

    public async Task<HttpContext?> GetRequestAsync()
    {
        var context = await _listener.GetContextAsync();

        var requestedUrl = context.Request.RawUrl;

        if (requestedUrl.StartsWith('/'))
        {
            requestedUrl = requestedUrl[1..];
        }

        var httpContext = new HttpContext
        {
            Id = Guid.NewGuid(),
            IpAdrress = context.Request.RemoteEndPoint.Address.MapToIPv4().ToString(),
            Request = new HttpRequest()
            {
                Url = requestedUrl
            },
            Response = new()
        };

        _unresponsedRequests.Add(httpContext.Id, context);

        return httpContext;
    }

    public void SendResponse(HttpContext context)
    {
        var httpListenerResponse = _unresponsedRequests[context.Id];
        string message = context.Response.Message is null ? "EmptyResponse!" : context.Response.Message;
        var buffer = Encoding.UTF8.GetBytes(message);
        httpListenerResponse.Response.OutputStream.Write(buffer, 0, buffer.Length);
        httpListenerResponse.Response.Close();
        _unresponsedRequests.Remove(context.Id);
    }
}
