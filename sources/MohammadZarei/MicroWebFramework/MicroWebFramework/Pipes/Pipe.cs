namespace MicroWebFramework.Pipes;

public abstract class Pipe(Action<Context> next)
{
    public Action<Context> _next = next;

    public abstract void Handle(Context context);
}

