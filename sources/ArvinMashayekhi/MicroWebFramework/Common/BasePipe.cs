
namespace MicroWebFramework.Common;
public abstract class BasePipe(Action<HttpContext> context)
{
    protected readonly Action<HttpContext> _context =context;
    public abstract void Handler(HttpContext httpContext);

}

