using MicroWebFrameworkConsole.Pipes;
using MicroWebFrameworkConsole;
using PipelineDesignPattern.Pipes;
using System.Net;
using System.Text;



var httpListener = new HttpListener();

var listeningPerefixes = "http://localhost:5005/";
httpListener.Prefixes.Add(listeningPerefixes);
Console.WriteLine($"Application is Listening to {listeningPerefixes}");
httpListener.Start();
var httpContext = httpListener.GetContext();
var hTTP = new HTTPContext();
//Service Restiction actives when HTTPContext.Ip
//hTTP.IP = "iran";
hTTP.Request=httpContext.Request;
hTTP.Url=httpContext.Request.Url.AbsolutePath.ToString();
var pipeLineBuilder = new PipeLineBuilder().AddPipe<LocationManagmentPipe>()
                            .AddPipe<AuthenticationPipe>()
                            .AddPipe<EndPointPipe>();

var pipeLine = pipeLineBuilder.Build();
var x=pipeLine.Invoke(hTTP);

var message = x;
var buffer = Encoding.UTF8.GetBytes(message);
httpContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
httpContext.Response.Close();


Console.ReadKey();










