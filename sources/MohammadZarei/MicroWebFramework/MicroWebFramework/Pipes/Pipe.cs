namespace MicroWebFramework.Pipes;

public abstract class Pipe(Action<Context> next)
{
    protected Action<Context> _next = next;

    public abstract void Handle(Context context);
}

