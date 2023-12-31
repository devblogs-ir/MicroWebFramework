// See https://aka.ms/new-console-template for more information

using System.Net;
using Controller;
using Framework;
using Pipeline;


var addr = "http://localhost:9823/";
var httplistener = new HttpListener();
httplistener.Prefixes.Add(addr);

try {
    httplistener.Start();
    Console.WriteLine("Start --- ");

  
        while (true)
        {
       
            HttpListenerContext context = await httplistener.GetContextAsync();
            HttpContext httpContext = new HttpContext { Request = context.Request, Response = context.Response };
            var pipes = new PipelineBuilder().AddPipe<ExceptionHandler>()
            .AddPipe<AuthenticationHandler>()
            .AddPipe<EndpoindHandler>().Build();
             pipes(httpContext);

        }
   
    
 }
catch (Exception e) {
      Console.WriteLine("Error "+ e.Message);
 }
finally { }
/*ProductController d = new ProductController();
await d.GetProductsAPIAsync();*/
Console.ReadKey();

