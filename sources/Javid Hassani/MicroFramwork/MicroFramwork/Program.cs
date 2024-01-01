// See https://aka.ms/new-console-template for more information
using MicroFramwork;
using MicroFramwork.Common;
using MicroFramwork.Pipes;
using System.Net;
var appBase = new ApplicationBase("http://localhost:4040/");
var listener = new HttpListener();
Console.WriteLine($"app started listening at {appBase.BaseUrl}");
listener.Prefixes.Add(appBase.BaseUrl);

listener.Start();

var httpContext = listener.GetContext();

ApplicationContext context = new(httpContext, appBase);

var app = new PipelineBuilder();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseMiddleware<RequestMappingMiddleware>();

app.Build(context);

Console.ReadKey();

