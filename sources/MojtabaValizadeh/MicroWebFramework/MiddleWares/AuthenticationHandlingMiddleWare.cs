

namespace MicroWebFramework;

public class AuthenticationHandlingMiddleWare:Pipe
{
    public AuthenticationHandlingMiddleWare(Action<HttpContext> next) : base(next)
    {
        _next = next;
    }

    public override void Handle(HttpContext httpContext)
    {
        var requestIp = httpContext.Request.RemoteEndPoint?.Address?.ToString();
        
        //this code block request that from 192.168.0.130 ip
        if(requestIp is "192.168.0.130")
        {
            throw new AccessDeniedExceptionHandler("Access is  Denied");
        }

        _next(httpContext);
    }
}