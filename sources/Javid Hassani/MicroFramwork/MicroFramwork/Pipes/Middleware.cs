namespace MicroFramwork.Pipes;

public abstract class Middleware
{
    public Action<ApplicationContext> _next;

    public Middleware(Action<ApplicationContext> next)
    {
        _next = next;
    }
    public abstract void Handle(ApplicationContext context);
}
