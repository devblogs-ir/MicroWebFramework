namespace MicroWebFramework;

public abstract class Pipe
{
    public Action<HttpContext> _next;

    public Pipe(Action<HttpContext> next)
    {
        _next = next;
    }

    public Pipe()
    {
        _next = null!;
    }

    public abstract void Handle(HttpContext httpContext);
}