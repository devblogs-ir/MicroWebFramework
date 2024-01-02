namespace PipelineDesignPattern.Pipelines;

public abstract class Pipe
{
    public Pipe()
    {
        _next = null!;
    }
    protected Action<HttpContext> _next;
    public Pipe(Action<HttpContext> next)
    {
        _next = next;
    }

    public abstract void Handle(HttpContext context);
}