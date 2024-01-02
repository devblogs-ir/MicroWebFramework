using MicroWebFramework.pipeline;

namespace MicroWebFramework.Middleware;

public class CorsHandler : Pipe
{
    public CorsHandler(Action<HttpContext> next) : base(next)
    {
    }

    public override void Handle(HttpContext httpContext)
    {
        _next(httpContext);
    }
}