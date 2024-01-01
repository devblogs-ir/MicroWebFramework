
using Dumpify;
using MicroWebFramework.Models;

namespace MicroWebFramework;

public class ExceptionHandlingPipe(Action<HttpContext> next) : Pipe(next)
{
    public override void Handle(HttpContext httpContext)
    {
        "starting ExceptionHandling".Dump();
        try
        {
            if (Next is not null) Next(httpContext);
        }
        catch (Exception ex)
        {
            ex.Message.Dump();
        }
        "End ExceptionHandling".Dump();

    }
}
