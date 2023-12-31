using Dumpify;
using MicroWebFramework;
using System.Net;

var httpPrefix = "http://localhost:7776/";
HttpListener httpListener = new HttpListener();
httpListener.Prefixes.Add(httpPrefix);

Console.WriteLine($"Listening on {httpPrefix} ...");

try
{
    httpListener.Start();
    while (true)
    {
        HttpListenerContext httpContext = httpListener.GetContext();

        Task.Run(() => HandleRequest(httpContext));
    }

}
catch (HttpListenerException ex)
{
    $"HttpListenerException: {ex.Message}".Dump();
}
finally
{
    httpListener.Stop();
}

void HandleRequest(HttpListenerContext httpContext)
{
    try
    {
        HttpContext request = new()
        {
            IP = httpContext.Request.RemoteEndPoint?.Address.ToString(),
            Url = httpContext.Request.RawUrl,
            Response = httpContext.Response,
            Request = httpContext.Request
        };

        var pipeline = new PipelineBuilder()
            .AddPipe(typeof(ExceptionHandlingPipe))
            .AddPipe(typeof(AuthenticationPipe))
            .AddPipe(typeof(EndPointPipe))
            .Build();

        pipeline(request);
    }
    catch (Exception ex)
    {
        $"Exception: {ex.Message}".Dump();
        httpContext.Response.StatusCode = 500;
    }
    finally
    {
        httpContext.Response.Close();
    }
}