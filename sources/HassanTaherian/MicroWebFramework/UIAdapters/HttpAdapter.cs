using Dumpify;
using System;
using System.Net;
using System.Text;

namespace MicroWebFramework;
public class HttpAdapter : IUiAdapter
{
    private readonly HttpListener _listener;
    private readonly Dictionary<Guid, HttpListenerContext> _unresponsedRequests;

    public HttpAdapter(string url)
    {
        url = FormtServerUrl(url);
        _listener = SetupHttpListener(url);
        $"HTTP Server is Listening at {url}".Dump();
        _unresponsedRequests = [];
    }

    private static string FormtServerUrl(string url)
    {
        return url.EndsWith('/') ? url : url + "/";
    }

    private static HttpListener SetupHttpListener(string url)
    {
        HttpListener httpListener = new();
        httpListener.Prefixes.Add(url);
        httpListener.Start();
        return httpListener;
    }

    public async Task<HttpContext?> GetRequestAsync()
    {
        var httpListenerContext = await _listener.GetContextAsync();

        var requestedUrl = GetUrl(httpListenerContext.Request);
        var clientIpAddress = httpListenerContext.Request.RemoteEndPoint.Address.MapToIPv4()
                                                                                .ToString();

        if (requestedUrl is null || clientIpAddress is null)
        {
            return null;
        }

        var httpContext = new HttpContext(ipAdrress: clientIpAddress, requestedUrl: requestedUrl);

        _unresponsedRequests.Add(httpContext.Id, httpListenerContext);

        return httpContext;
    }

    private static string? GetUrl(HttpListenerRequest request)
    {
        var requestedUrl = request.RawUrl;

        if (requestedUrl is null)
        {
            return null;
        }

        if (requestedUrl.StartsWith('/'))
        {
            requestedUrl = requestedUrl[1..];
        }

        return requestedUrl;
    }

    public void SendResponse(HttpContext context)
    {
        var response = _unresponsedRequests[context.Id].Response;

        string message = context.Response.Message is null ? "EmptyResponse!" : context.Response.Message;
        WriteMessageToResponseStream(response, message);

        _unresponsedRequests.Remove(context.Id);
    }

    private static void WriteMessageToResponseStream(HttpListenerResponse response, string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.Close();
    }
}
