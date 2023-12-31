using System.Net;

var httpListener = new HttpListener();

var httpPrefix = $"http://localhost:9824/";

httpListener.Prefixes.Add(httpPrefix);

Console.WriteLine($"start listening to {httpPrefix} ...");

try
{
    httpListener.Start();

    while (true)
    {
        HttpListenerContext httpContext = httpListener.GetContext();

        Task.Run(() => HandleRequest(httpContext));
    }
}
catch
{

}
finally
{

}