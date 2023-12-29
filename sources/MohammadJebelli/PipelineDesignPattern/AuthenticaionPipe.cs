using Dumpify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern
{
    public class AuthenticaionPipe : Pipe
    {
        public AuthenticaionPipe(Action<HttpContext> next) : base(next)
        {
        }

        public override void Handle(HttpContext httpContext)
        {
            httpContext.IP.Dump("Start auth...");

            if (httpContext.IP.StartsWith("185"))
            {
                throw new BannedIpException("Sorry you are accessing from Islamic Republic.");
            }

            httpContext.IP.Dump("End auth...");

            if (httpContext is not null)
                _next(httpContext);
        }
    }
}
