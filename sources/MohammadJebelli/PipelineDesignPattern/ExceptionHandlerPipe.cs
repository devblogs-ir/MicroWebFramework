using Dumpify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern
{
    public class ExceptionHandlerPipe : Pipe
    {
        public ExceptionHandlerPipe(Action<HttpContext> next) : base(next)
        {
        }

        public override void Handle(HttpContext httpContext)
        {
            try
            {
                "Start exception handling...".Dump();

                if (_next is not null)
                    _next(httpContext);

            }
            catch (BannedIpException ex)
            {
                "Exception catched: ".Dump(ex.Message);
            }

        }
    }
}
    