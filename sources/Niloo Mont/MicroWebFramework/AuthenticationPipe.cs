namespace MicroWebFramework;
public class AuthenticationPipe : BasePipe
{
    public AuthenticationPipe(Action<HttpContext> next) : base(next)
    {
    }

    public override void Handle(HttpContext context)
    {
        $"Authentication Started for {context.IP}".Dump();
        if (_next is not null) _next(context);
        $"Authentication Ended for {context.IP}".Dump();
    }
}
