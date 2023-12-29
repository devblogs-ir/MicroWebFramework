using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern
{
    public abstract class Pipe
    {
        protected Action<HttpContext> _next;
        public Pipe(Action<HttpContext> next)
        {
            _next=next;   
        }

        public abstract void Handle(HttpContext httpContext);
    }
}
