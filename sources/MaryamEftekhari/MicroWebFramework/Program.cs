using MicroWebFramework;
using MicroWebFramework.Context;
using System.Net;
using System.Text.Json;


var httplistner = new HttpListener();
var httpPrefix = "http://localhost:6090/";
httplistner.Prefixes.Add(httpPrefix);
httplistner.Start();
var httpContext = httplistner.GetContext();
var context = new HttpContext
{
    Url = httpContext.Request.Url!.ToString(),
    Request = httpContext.Request, 
    IP = httpContext.Request.UserHostAddress
};

var pipeLineBuilder = new PipeLineBuilder()
    .AddPipe(typeof(ExceptionHandlingPipe))
    .AddPipe(typeof(AuthenticationPipe))
    .AddPipe(typeof(EndPointPipe))
    .Build();

pipeLineBuilder(context);
var serilizeResponse = JsonSerializer.Serialize(context.Response);
byte[] buffer = System.Text.Encoding.UTF8.GetBytes(serilizeResponse);
HttpListenerResponse response = httpContext.Response;
response.ContentLength64 = buffer.Length;
Stream output = response.OutputStream;
output.Write(buffer, 0, buffer.Length);
output.Close();
httplistner.Stop();