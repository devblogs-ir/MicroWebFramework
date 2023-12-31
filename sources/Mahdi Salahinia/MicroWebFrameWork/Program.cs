using Dumpify;
using MicroWebFrameWork;
using System.Net;

var httpListener = new HttpListener();

var httpPrefix = $"http://localhost:9824/";

httpListener.Prefixes.Add(httpPrefix);

$"start listening to {httpPrefix} ...".Dump();

try
{
    httpListener.Start();
    
    while (true)
    {
        HttpListenerContext httpContext = httpListener.GetContext();

        PipeLineRunner.RunPipeLines(httpContext);
    }
}
catch (HttpListenerException ex)
{
    $"{ex.Message}".Dump();
}
finally
{
    httpListener.Stop();
}