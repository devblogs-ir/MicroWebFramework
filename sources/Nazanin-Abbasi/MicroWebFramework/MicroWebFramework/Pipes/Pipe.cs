using MicroWebFramework.Models;

namespace MicroWebFramework.Pipes;
public abstract class Pipe
{
    public Pipe()
    {
        _next = null!;
    }
    public Pipe(Action<HttpContext> next)
    {
        _next = next;
    }
    public Action<HttpContext> _next;

    public abstract void Handle(HttpContext httpContext);
}
