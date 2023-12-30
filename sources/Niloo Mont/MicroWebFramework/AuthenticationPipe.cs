using System.Text;

namespace MicroWebFramework;

public class AuthenticationPipe : BasePipe
{
    public AuthenticationPipe(Action<HttpContext> next) : base(next)
    {
    }

    public override void Handle(HttpContext context)
    {        
        context.Response.OutputStream.Write(
                EncodingService.GetBytes($"Authentication Started for {context.IP}"));
        if (_next is not null) _next(context);
        context.Response.OutputStream.Write(
                EncodingService.GetBytes($"Authentication Ended for {context.IP}"));    
    }
}
