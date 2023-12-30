// See https://aka.ms/new-console-template for more information
using MicroApiFramework;
using System.Net;
using System.Text;
using static MicroApiFramework.Framework;

Console.WriteLine("Hello, World!");

var httpListener = new HttpListener();
string address = "http://localhost:4545/";
httpListener.Prefixes.Add(address);
httpListener.Start();
var httpContext = httpListener.GetContext();

Console.ReadLine();


Framework framework = new();

var request = new HttpContext
{
    IP = "37.255.130.023",
    Url = httpContext.Request.RawUrl == "/" ?"Product/GetProductById/1": httpContext.Request.RawUrl
};


var firstpipe = new PipelineBuilder()
    .AddPipe<ExceptionHandlingPipe>()
    .AddPipe<AuthenticationPipe>()
    .AddPipe<EndPointPipe>()
    .Build();

firstpipe.Handle(request);

string message = request.Response;
var buffer = Encoding.UTF8.GetBytes(message);
httpContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
httpContext.Response.Close();


//var ep = new EndPointPipe(null!);
//var au = new AuthenticationPipe(ep.Handle);
//var eh = new ExceptionHandlingPipe(au.Handle);
//eh.Handle(requestGetAllProduct);





