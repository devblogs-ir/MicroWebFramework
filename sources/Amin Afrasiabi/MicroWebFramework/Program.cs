using PipelineDesignPattern;
using PipelineDesignPattern.Pipelines;
using PipelineDesignPattern.Pipelines.Builder;
using System.Net;
using System.Text;


var pipe = new PipelineBuilder().AddPipe<ExceptionHandlingPipe>()
                        .AddPipe<AuthenticationPipe>()
                        .AddPipe<AuthenticationPipe>()
                        .AddPipe<AuthenticationPipe>()
                        .AddPipe<EndpointHandlingPipe>()
                        .Build();

using var httpListener = new HttpListener();
var prefixes = "http://localhost:5006/";
httpListener.Prefixes.Add(prefixes);
Console.WriteLine($"Start Listening To {prefixes}...\n");

httpListener.Start();

try
{
    while (httpListener.IsListening)
    {
        var context = await httpListener.GetContextAsync();

        ArgumentNullException.ThrowIfNull(context.Request.RawUrl);

        if (IsHomePage(context.Request.RawUrl))
        {
            var message = "Welcome to MicroWebFramework Home Page";
            var buffer = Encoding.UTF8.GetBytes(message);
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.Close();
            continue;
        }

        var userRequest = new HttpContext
        {
            Url = context.Request.RawUrl,
            IP = context.Request.RemoteEndPoint.Address.ToString(),
            Request = context.Request,
            Response = context.Response,
        };

        pipe(userRequest);
    }
}
finally
{
    httpListener.Stop();
}

bool IsHomePage(string url)
{
    if (string.IsNullOrWhiteSpace(url.Split('/')[1])
            || url.Split('/')[1] is "Home" or "home")
    {
        return true;
    }
    return false;
}