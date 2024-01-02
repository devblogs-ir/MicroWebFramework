using MicroFramwork.Pipes;

namespace MicroFramwork;

public class PipelineBuilder
{
    private List<Type> _types = [];

    public PipelineBuilder UseMiddleware<T>() where T : class
    {
        _types.Add(typeof(T));
        return this;
    }

    public void Build(ApplicationContext context)
    {
        Action<ApplicationContext> next = null!;

        for (int i = _types.Count - 1; i >= 0; i--)
        {
            var instance = Activator.CreateInstance(_types[i], new[] { next }) as Middleware;

            next = instance!.Handle;

        }
        next.Invoke(context);
    }
}
