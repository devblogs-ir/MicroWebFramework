namespace MicroWebFrameWork.Pipes;

public abstract class BasePipe
{
    protected readonly Action<HttpContext> _next;

    public BasePipe(Action<HttpContext> next)
    {
        _next = next;
    }

    public abstract void HandlePipe(HttpContext context);
}
