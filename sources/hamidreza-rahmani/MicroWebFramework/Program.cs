// See https://aka.ms/new-console-template for more information

using System.Net;
using MicroWebFramework;
using MicroWebFramework.Middleware;
using MicroWebFramework.pipeline;

var pool = new ObjectPool<StateObject>(() => new StateObject(), 2);
;
var httpListener = new HttpListener();
var semaphore = new SemaphoreSlim(10, 10);

var localhostPrefix = "http://localhost:3998/";
httpListener.Prefixes.Add(localhostPrefix);
httpListener.Start();

Console.WriteLine($"Start Listening to {localhostPrefix}...");

while (true)
{
    var context = httpListener.GetContext();
    var state = pool.Get();
    state.Context = context;
    semaphore.Wait();
    Task.Run(() => ProcessClient(state, context));
}

async Task ProcessClient(StateObject state, HttpListenerContext context)
{
    try
    {
        var httpContext = new HttpContext { Request = context.Request, Response = context.Response };
        PiplineBuilder pipelineBuilder = new();
        var pipeline = pipelineBuilder
            .Add<AuthenticationHandler>()
            .Add<CorsHandler>()
            .Add<EndpointHandler>()
            .Build();

        pipeline(httpContext);
        pool.Return(state);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        pool.Return(state);
    }
    finally
    {
        semaphore.Release(); // Release the semaphore when the request is processed
    }
}