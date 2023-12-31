using Dumpify;
using PipelineDesignPattern.Exceptions;

namespace PipelineDesignPattern.Pipelines;

public class AuthenticationPipe : Pipe
{
    public AuthenticationPipe() : base() { }
    public AuthenticationPipe(Action<HttpContext> next) : base(next) { }

    public override void Handle(HttpContext context)
    {
        "Start Authentication...".Dump();
        ArgumentNullException.ThrowIfNull(context);

        if (context.Country is CountryIPAddress.Iran)
        {
            throw new IranianIPBlockedException(message: "Iranian IPs are blocked");
        }

        if (_next is not null) _next(context);
    }
}
