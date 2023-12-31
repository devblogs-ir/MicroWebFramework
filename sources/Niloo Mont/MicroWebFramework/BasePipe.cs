namespace MicroWebFramework;
public abstract class BasePipe
{
    protected readonly Action<HttpContext> _next;
    public BasePipe(Action<HttpContext> next)
    {
        _next = next;
    }
    public abstract void Handle(HttpContext context);
}
