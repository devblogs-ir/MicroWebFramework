using MicroWebFramework.Http;

namespace MicroWebFramework.Builder;
public class PipelineBuilder
{
    private readonly List<Type> _pipes;
    public PipelineBuilder() => _pipes = new();
    private void Add(Type pipe)
    {
        _pipes.Add(pipe);
    }
    public PipelineBuilder Add<TType>() where TType : MiddlewareBase
    {
        Add(typeof(TType));
        return this;
    }
    public RequestDelegate Build()
    {
        if (_pipes.Count == 0) throw new Exception();

        var createdPipe = Activator.CreateInstance(_pipes[_pipes.Count - 1]) as MiddlewareBase;

        for (int i = _pipes.Count - 2; i > 0; i--)
        {
            createdPipe = Activator.CreateInstance(_pipes[i], (RequestDelegate)createdPipe.InvokeAsync) as MiddlewareBase;
        }
        var firstPipe = Activator.CreateInstance(_pipes[0], (RequestDelegate)createdPipe.InvokeAsync) as MiddlewareBase;

        return firstPipe.InvokeAsync;
    }
}