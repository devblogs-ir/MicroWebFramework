

namespace MicroWebFramework.Pipeline;

public abstract class BasePipe
{
    public Action<HttpContext> Next { get; set; }

    protected BasePipe(Action<HttpContext> next) => Next = next;

    public abstract void Invoke(HttpContext context);
}

