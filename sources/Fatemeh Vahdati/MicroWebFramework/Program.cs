// See https://aka.ms/new-console-template for more information

using MicroWebFramework.Pipes;
using System.Net;
using System.Text;

var httplistener = new HttpListener();

var prefix = "http://localhost:6051/";
httplistener.Prefixes.Add(prefix);
Console.WriteLine($"Start listening to {prefix} ...");
httplistener.Start();
 var HttpListenerContext = httplistener.GetContext();

//var message = "Hello web application world";
//var buffer = Encoding.UTF8.GetBytes(message);
//HttpListenerContext.Response.OutputStream.Write(buffer, 0, buffer.Length);



var firstHandle = new PipelineBuilder()
            .AddPipe<ExceptionHandlingPipe>()
            .AddPipe<AuthenticationPipe>()
            .AddPipe<EndpointPipe>()
            .Build();
firstHandle(HttpListenerContext);
HttpListenerContext.Response.Close();
Console.ReadLine();

