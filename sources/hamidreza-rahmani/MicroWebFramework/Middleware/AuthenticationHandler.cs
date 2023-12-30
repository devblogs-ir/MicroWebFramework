using MicroWebFramework.pipeline;

namespace MicroWebFramework.Middleware;

public class AuthenticationHandler : Pipe
{
    public AuthenticationHandler(Action<HttpContext> next) : base(next)
    {
    }

    public override void Handle(HttpContext httpContext)
    {
        var ipAddress = httpContext?.Request?.RemoteEndPoint?.Address.ToString();

        if (ipAddress is null) throw new BadRequestExeption("BadRequest Exeption");

        if (ipAddress.StartsWith("200")) throw new BlockedIpExeption("Blocked Ip Exeption");

        _next(httpContext);
    }
}