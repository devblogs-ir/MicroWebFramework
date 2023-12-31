using HttpSelfHostConsole.Framework.Models;
using System;

namespace HttpSelfHostConsole.Framework.Pipelines
{
    public abstract class Pipe
    {
        public Pipe(Action<HttpContext> next)
        {
            _next = next;
        }
        public Action<HttpContext> _next;

        public abstract void Handler(HttpContext httpContext);
    }
}
