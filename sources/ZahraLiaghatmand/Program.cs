using PipelineDesignPattern;
using PipelineDesignPattern.Pipes;
using PipelineDesignPattern.Exceptions;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Metadata;
using System.Net;
using System.Text;

var httpListener = new HttpListener();
var httpPrefixe = "http://localhost:4545/";
httpListener.Prefixes.Add(httpPrefixe);
Console.WriteLine($"start listening to {httpPrefixe} ...");
httpListener.Start();

while (1==1){

var httpContext = httpListener.GetContext();
Console.WriteLine($"start listening to {httpContext} ...");

    Framework framework = new();
    HttpContext request = new()
    {
        IP = "123.185.20.177",
        Url = httpContext.Request.RawUrl
    };
    var firstHandler = new Framework.PipelineBuilder().
                            AddPipe<ExceptionHandlingPipe>().
                            AddPipe<AuthenticationPipe>().
                            AddPipe<EndPointPipe>().
                            build();
    firstHandler(request);

    var message = request.Response;
    var buffer = Encoding.UTF8.GetBytes(message);
    httpContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
    httpContext.Response.Close();
}