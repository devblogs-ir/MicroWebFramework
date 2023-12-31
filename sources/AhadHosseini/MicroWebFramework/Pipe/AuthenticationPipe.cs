using Dumpify;
using MicroWebFramework.Models;
namespace MicroWebFramework;


public class AuthenticationPipe(Action<HttpContext> next) : Pipe(next)
{
    public override void Handle(HttpContext httpContext)
    {
        "staring Authentication ".Dump();

        if (httpContext.Request.RemoteEndPoint.ToString().StartsWith("188"))
            throw new Exception($"The {httpContext.Request.RemoteEndPoint.ToString()} Invalid Ip");
        else
            if (_next != null) _next(httpContext);

        "End Authentication".Dump();
    }
}
