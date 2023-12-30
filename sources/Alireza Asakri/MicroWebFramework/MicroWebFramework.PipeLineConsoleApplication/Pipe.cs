using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.PipeLineConsoleApplication
{
    public abstract class Pipe
    {
        public Pipe()
        {
            _next = null!;
        }
        public Pipe(Action<HttpContext> next)
        {
            _next = next;
        }
        public Action<HttpContext> _next;
        public abstract void Handle(HttpContext httpContext);
    }
}
