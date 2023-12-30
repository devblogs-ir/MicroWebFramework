namespace PipelineDesignPattern;

public abstract class Pipe
{
    protected Action<HttpContext> _next;
    public Pipe(Action<HttpContext> next)
    {
        if (next is not null)
            _next = next;
    }
    public Pipe(){ }
    public abstract void Handle(HttpContext context);
}