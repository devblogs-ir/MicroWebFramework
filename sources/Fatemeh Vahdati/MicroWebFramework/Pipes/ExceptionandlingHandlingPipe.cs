// See https://aka.ms/new-console-template for more information

using System.Net;

namespace MicroWebFramework.Pipes
{
    public class ExceptionHandlingPipe(Action<HttpListenerContext> next) : Pipe(next)
    {

        public override void Handle(HttpListenerContext HttpListenerContext)
        {
            try
            {
                Console.WriteLine("ExceptionHandling Starating...");
                if (_next is not null)
                    _next(HttpListenerContext);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Authentication exception: {ex.Message}");
            }
        }
    }
}