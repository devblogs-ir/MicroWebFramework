using System.Net;
using System.Text;
using Dumpify;
using Newtonsoft.Json;
using MicroWebFramework;

var prefix = "http://localhost:8074/";
var httpListener = new HttpListener();
httpListener.Prefixes.Add(prefix);
try
{
    httpListener.Start();
    "Service Started:".Dump($"Your service start at  {prefix} ");
}
catch (HttpListenerException ex)
{
    if (ex.ErrorCode == 183)
    {
        "Invalid Port".Dump($"Address {prefix} used by another program");
    }
    else
        "Error:".Dump($"An error occurred when starting service.");
}
catch (Exception ex)
{
    "Error:".Dump($"An error occurred when starting service.");
}

// Handle incoming requests
Task.Run(async () =>
{
    while (true)
    {
        try
        {
            HttpListenerContext context = await httpListener.GetContextAsync();

            HttpContext httpContext = new HttpContext { Request = context.Request,Response = context.Response};
            var handler=new PiplineBuilder()
                .Add<ExceptionHandlingMiddleWare>()
                .Add<AuthenticationHandlingMiddleWare>()
                .Add<CorsHandlingMiddleWare>()
                .Add<EndpointHandlingMiddleWare>()
                .Build();
            handler(httpContext);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling request: {ex.Message}");
        }
    }
    
});
Console.WriteLine("Press any key to stop the listener.");
Console.ReadKey();

httpListener.Stop();

    




