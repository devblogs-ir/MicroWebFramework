using HttpSelfHostConsole.Framework.Models;
using System;

namespace HttpSelfHostConsole.Framework.Pipelines
{
    public class ExceptionHandlingPipe: Pipe
    {
        public ExceptionHandlingPipe(Action<HttpContext> next) : base(next)
        {

        }

        public ExceptionHandlingPipe() : base(null)
        {

        }

        public override void Handler(HttpContext httpContext)
        {
            try
            {
                Console.WriteLine($"Starting Exception Handeling... ({httpContext.IP})");
                if (_next != null)
                    _next(httpContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine($"Finish Exception Handeling.  ({httpContext.IP})");
            }
        }
    }
}
