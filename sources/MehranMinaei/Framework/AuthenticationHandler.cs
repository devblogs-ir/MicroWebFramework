using Pipeline;

namespace Framework;

public class AuthenticationHandler :Pipe {
  
    public AuthenticationHandler(Action<HttpContext> next) :base(next)
    {
        _next = next;
    }

    public AuthenticationHandler()
    {
        _next = null;
    }

    public override void Handle(HttpContext httpContext)
    {
        var requestIp = httpContext.Request.RemoteEndPoint?.Address?.ToString();
        
        //block request
        if(requestIp is "172.23.0.11")
        {
            throw new Exception("Access is  Denied");
        }

        _next(httpContext);
    }
}