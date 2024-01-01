using System.Diagnostics;
using System.Net;
using System.Text;
using MicroWebFramework.Middlewares;
using MicroWebFramework.Models;
using MicroWebFramework.PipLines;
using EndPoint = MicroWebFramework.Middlewares.EndPoint;

namespace MicroWebFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {

            HttpListener listener = new HttpListener();
            var prefix = "http://localhost:8000/";
            listener.Prefixes.Add(prefix);
            $"Now listening on {prefix}".Dump();
            listener.Start();

            //open browser
            ProcessStartInfo processStart = new ProcessStartInfo
            {
                FileName = prefix,
                UseShellExecute = true
            };
            Process.Start(processStart);


            while (true)
            {
                var httpContext = listener.GetContext();

                if (httpContext.Request.Url.AbsoluteUri is null)
                {
                    var message = "Welcome dear friend";
                    var buffer = Encoding.UTF8.GetBytes(message);
                    httpContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    httpContext.Response.Close();
                }
                else
                {
                  var pipLine = new PipeLineBuilder()
                        .AddPipe<ExceptionHandling>()
                        .AddPipe<Authentication>()
                        .AddPipe<EndPoint>()
                        .Build();
                  pipLine.Handle(httpContext);
                }
            }
        }
    }
}
