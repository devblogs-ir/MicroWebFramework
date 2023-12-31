using Dumpify;
using MicroWebFramework.Builders;
using MicroWebFramework.Models;
using MicroWebFramework.Pipes;
using System.Diagnostics;
using System.Net;


HttpListener listener = new HttpListener();
var prefix = "http://localhost:8080/";
listener.Prefixes.Add(prefix);

$"Listening to {prefix}".Dump();

ProcessStartInfo processStart = new ProcessStartInfo
{
    FileName = prefix,
    UseShellExecute = true
};
Process.Start(processStart);

listener.Start();
while (true)
{
    HttpListenerContext httpListenerContext = listener.GetContext();

    HttpContext request = new()
    {
        IP = httpListenerContext.Request.RemoteEndPoint?.Address.ToString(),
        Url = httpListenerContext.Request.RawUrl,
        Response = httpListenerContext.Response,
        Request = httpListenerContext.Request
    };
    try
    {
        Action<HttpContext> pipe = new PipelineBuilder()
                                    .AddPipe<AuthenticationPipe>()
                                    .AddPipe<ExceptionHandlingPipe>()
                                    .AddPipe<EndPointPipe>()
                                    .Build();

        pipe(request);
    }
    catch (Exception ex)
    {
        "Exception".Dump(ex.Message);
    }

}