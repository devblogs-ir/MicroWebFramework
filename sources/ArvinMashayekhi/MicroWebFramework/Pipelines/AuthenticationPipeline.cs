using Dumpify;
using MicroWebFramework.Common;
using MicroWebFramework.Exeptions;

namespace MicroWebFramework.Pipelines;
public class AuthenticationPipeline : BasePipe
{
    public AuthenticationPipeline(Action<HttpContext> context) : base(context)
    {
    }

    public override void Handler(HttpContext httpContext)
    {
        "check your IP Address".Dump();
        if (string.Compare(httpContext.IP, "192.168.0.130", StringComparison.OrdinalIgnoreCase) == 0)
        {
            throw new IPExeptionHandler(httpContext.IP);
        }
        else
            _context(httpContext);

        "End of checking IP address".Dump();
    }
}
