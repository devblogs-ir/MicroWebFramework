using System.Text;

namespace MicroWebFramework;

public class ExceptionHandlingPipe : BasePipe
{
    public ExceptionHandlingPipe(Action<HttpContext> next) : base(next)
    {
    }

    public override void Handle(HttpContext context)
    {
        try
        {
            if (context.IP is "Iran")
                throw new BannedIPException(context.IP);
            if (_next is not null) _next(context);
        }
        catch (BannedIPException ex)
        {
            context.Response.OutputStream.Write(
                EncodingService.GetBytes(ex.Message));
        }
    }
}
