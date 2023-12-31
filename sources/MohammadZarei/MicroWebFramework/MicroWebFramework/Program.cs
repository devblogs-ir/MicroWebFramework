using MicroWebFramework.Pipes;
using MicroWebFramework;
using System.Net;

var httpListener = new HttpListener();
httpListener.Prefixes.Add(ApplicationConsts.BaseAddress);

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"starting listening to port {ApplicationConsts.BaseAddress}");
Console.ResetColor();

httpListener.Start();
var httpContext = httpListener.GetContext();

var getUserRequest1 = new Context
{
    HttpListenerContext = httpContext,
};

var builder = new PipelineBuilder()
    .WithType(typeof(ExceptionHandler))
    .WithType(typeof(AuthenticationHandler))
    .WithType(typeof(EndpointHandler))
    .Build();

builder.Invoke(getUserRequest1);

