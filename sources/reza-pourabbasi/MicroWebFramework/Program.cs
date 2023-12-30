using System.Net;
using MicroWebFramework.Builder;
using MicroWebFramework.Middlewares;


HttpListener httpListener = new();
var server = "http://localhost:14301/";
httpListener.Prefixes.Add(server);
Console.WriteLine($"start listening to {server} ...");

httpListener.Start();

try
{
    while (true)
    {
        var httpContext = await httpListener.GetContextAsync();

        PipelineBuilder pipelineBuilder = new();

        var pipeLine = pipelineBuilder
        .UseExceptionHanding()
        .UseAuthentication()
        .UseEndPoint()
        .Build();

        await pipeLine(httpContext);

    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    httpListener.Stop();
    httpListener.Close();
}




