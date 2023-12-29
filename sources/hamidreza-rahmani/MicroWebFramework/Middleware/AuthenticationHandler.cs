using Dumpify;
using MicroWebFramework.pipeline;

namespace MicroWebFramework.Middleware;

public class AuthenticationHandler : Pipe
{
    public AuthenticationHandler(Action<HttpContext> next) : base(next)
    {
    }

    public override void Handle(HttpContext httpContext)
    {
        httpContext.Request.RemoteEndPoint.Address.ToString().Dump("Start auth...");

        string ipAddress = httpContext.Request?.RemoteEndPoint?.Address?.ToString();
        
        if (ipAddress is null  )
        {
            
        }
        if (httpContext.IP.StartsWith("185"))
            throw new BannedIpException("Sorry you are accessing from Islamic Republic.");

        httpContext.IP.Dump("End auth...");
        _next(httpContext);
    }
}