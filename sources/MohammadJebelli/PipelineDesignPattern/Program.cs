using Dumpify;
using PipelineDesignPattern;
using System.Net;
using System.Text;

var httpListener = new HttpListener();

var localhostPrefix = "http://localhost:3998/";

httpListener.Prefixes.Add(localhostPrefix);

httpListener.Start();

Console.WriteLine($"Start Listening to {localhostPrefix}...");

while (true)
{
    var context = httpListener.GetContext();
    var requestUrl = context.Request.Url.AbsolutePath;

    // ignore favicon request for chrome
    if (requestUrl.Contains("favicon.ico"))
    {
        context.Response.StatusCode = 404;
        context.Response.Close();

    }

    var localPath = context.Request.Url.LocalPath;


    var httpContext1 = new HttpContext()
    {
        IP = "216.239.38.120",
        Url = context.Request.Url.LocalPath
    };

    var handler = new PipeBuilder()
        .AddPipe<ExceptionHandlerPipe>()
        .AddPipe<AuthenticaionPipe>()
        .AddPipe<EndPointPipe>()
        .Build();


    handler(httpContext1);

    var buffer = Encoding.UTF8.GetBytes(httpContext1.httpResponse);

    context.Response.OutputStream.Write(buffer, 0, buffer.Length);

    context.Response.Close();

}







