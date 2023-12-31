
namespace MicroWebFramework.Common;
public abstract class BasePipe
{
    protected readonly Action<HttpContext> _context;
    public BasePipe(Action<HttpContext> context)
    {
        _context = context;
    }
    public abstract void PipelineHandler(HttpContext httpContext);

}

