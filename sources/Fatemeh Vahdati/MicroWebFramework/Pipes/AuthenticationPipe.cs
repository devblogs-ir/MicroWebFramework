
using System.Net;

namespace MicroWebFramework.Pipes
{
    public class AuthenticationPipe(Action<HttpListenerContext> next) : Pipe(next)
    {
        public override void Handle(HttpListenerContext HttpListenerContext)
        {
            Console.WriteLine($"Authentications starting...");
            try
            {
                if (_next is not null)
                    _next(HttpListenerContext);
            }
            catch (Exception e)
            {
                throw new Exception($"Authentication exception : {e.Message}");
            }
        }
    }
}