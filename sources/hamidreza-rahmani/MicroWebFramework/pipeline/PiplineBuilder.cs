namespace MicroWebFramework.pipeline;

public class PiplineBuilder
{
    private readonly List<Type> _pips = new();

    public PiplineBuilder Add(Type pipe)
    {
        _pips.Add(pipe);
        return this;
    }

    public PiplineBuilder Add<TType>() where TType : Pipe
    {
        return Add(typeof(TType));
    }

    public Action<HttpContext> Build()
    {
        var currentPipe = (Pipe)Activator.CreateInstance(_pips[^1], null)!;
        for (var i = _pips.Count() - 1; i > 0; i--)
            currentPipe = (Pipe)Activator.CreateInstance(_pips[i - 1], currentPipe.Handle)!;

        return currentPipe.Handle;
    }
}