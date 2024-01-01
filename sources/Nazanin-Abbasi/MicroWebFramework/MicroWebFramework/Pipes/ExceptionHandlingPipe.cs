using Dumpify;
using MicroWebFramework.Models;

namespace MicroWebFramework.Pipes;

public class ExceptionHandlingPipe(Action<HttpContext> next) : Pipe(next)
{
    public override void Handle(HttpContext httpContext)
    {
        try
        {
            "Start ExceptionHandling...".Dump("ExceptionHandling");

            if (_next is not null) _next(httpContext);

        }
        catch (InvalidIPException ex)
        {
            ex.Message.Dump();
        }
    }
}