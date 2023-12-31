// See https://aka.ms/new-console-template for more information

using MicroWebFrameWork;
using System.Net;


HttpListener listener = new ();
listener.Prefixes.Add("http://localhost:8080/");
listener.Start();


while (true)
{
    
    HttpListenerContext listenerContext = listener.GetContext();
    HttpContext request1 = new()
    {
        IP = "102.15.0.0",
        Url = listenerContext.Request?.Url?.ToString(),
        ListenerContext = listenerContext
    };
    var pipeline = new PipelineBuilder(request1)
       .AddPipe(typeof(ExceptionHandlingPip))
       .AddPipe(typeof(AuthenticationPip))
       .AddPipe(typeof(EndpointPip))
       .Build((ctx) => { });
    listenerContext.Response.OutputStream.Close();
    

}
 
listener.Stop();


Console.ReadLine();