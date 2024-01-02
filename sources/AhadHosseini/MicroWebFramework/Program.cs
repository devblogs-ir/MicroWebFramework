using Dumpify;
using MicroWebFramework;
using MicroWebFramework.Models;
using System;
using System.Net;
using System.Reflection;

const int port = 9585;

string prefix = $"http://127.0.0.1:{port}/";
HttpListener httpListener = new ();
httpListener.Prefixes.Clear();
httpListener.Prefixes.Add(prefix);
try
{
    httpListener.Start();
    $"Enter url sample =>  {prefix}Products/GetByID/2 ".Dump();
    while (httpListener.IsListening)
    {
        while (true)
        {
            Console.WriteLine("Press key Q to stop the listener. Press Any key to other Request .");
            var Key = Console.ReadKey();
            if (Key.Key.ToString() == "Q") httpListener.Stop();

            var Context = await httpListener.GetContextAsync();
            Console.WriteLine(Context.Request.Url);
            HttpContext httpContext = new HttpContext { Request = Context.Request, Response = Context.Response };
            var pipeLine = new PipeLineBuilder()
                .AddPipe<ExceptionHandlingPipe>()
                 .AddPipe<AuthenticationPipe>()
                .AddPipe<EndPointPipe>()
                .Build();

            pipeLine(httpContext);
           

        }

    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    httpListener.Stop();
}
