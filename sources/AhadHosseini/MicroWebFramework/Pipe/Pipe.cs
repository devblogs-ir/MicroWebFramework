using MicroWebFramework.Models;

namespace MicroWebFramework;

public abstract class Pipe
{
    public Pipe()
    {
        Next = null!;
    }
    public Pipe(Action<HttpContext> next)
    {
        Next = next;
    }
    public Action<HttpContext> Next;

    public abstract void Handle(HttpContext httpContext);
}
