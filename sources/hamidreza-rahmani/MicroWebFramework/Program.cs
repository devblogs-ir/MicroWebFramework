// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Text;
using MicroWebFramework;

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
    Task.Run(() => ProcessClient(state));
}

async Task ProcessClient(StateObject state)
{
    try
    {
        var context = state.Context;
        var output = context.Response.OutputStream;

        //response


        var response = "<html><body><h1>Hello, World!</h1></body></html>";
        var buffer = Encoding.UTF8.GetBytes(response);

        context.Response.ContentType = "text/html";
        context.Response.ContentLength64 = buffer.Length;

        await output.WriteAsync(buffer, 0, buffer.Length);

        output.Close();
        context.Response.Close();
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