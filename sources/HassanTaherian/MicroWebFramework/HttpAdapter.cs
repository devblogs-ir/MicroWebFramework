using Dumpify;
using System.Net;
using System.Text;

namespace MicroWebFramework;
public class HttpAdapter : IUiAdapter
{
    private readonly HttpListener _listener;
    private Dictionary<int, HttpListenerContext> _unresponsedRequests;

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
            Id = 1,
            IpAdrress = context.Request.RemoteEndPoint.Address.MapToIPv4().ToString(),
            Request = new HttpRequest()
            {
                Url = requestedUrl
            }
        };

        _unresponsedRequests.Add(httpContext.Id, context);

        return httpContext;
    }

    public void SendResponse(HttpContext context)
    {
        throw new NotImplementedException();
    }
}
