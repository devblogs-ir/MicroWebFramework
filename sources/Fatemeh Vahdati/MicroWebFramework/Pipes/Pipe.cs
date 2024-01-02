// See https://aka.ms/new-console-template for more information

using System.Net;

namespace MicroWebFramework.Pipes
{
    public abstract class Pipe
    {
        public Pipe()
        {
            _next = null!;
        }

        public Pipe(Action<HttpListenerContext> next)
        {
            _next = next;
        }
        public Action<HttpListenerContext> _next;
        public abstract void Handle(HttpListenerContext HttpListenerContext);
    }
}