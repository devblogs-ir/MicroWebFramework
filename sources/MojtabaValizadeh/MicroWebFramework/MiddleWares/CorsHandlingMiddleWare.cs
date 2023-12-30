using Dumpify;

namespace MicroWebFramework;

public class CorsHandlingMiddleWare:Pipe
{
    public CorsHandlingMiddleWare(Action<HttpContext> next) : base(next)
    {
        _next = next;
    }
    public override void Handle(HttpContext httpContext)
    {
        "CORS".Dump("Start CORS MiddleWare...");
        "CORS".Dump("End CORS MiddleWare...");
        _next(httpContext);
    }
}