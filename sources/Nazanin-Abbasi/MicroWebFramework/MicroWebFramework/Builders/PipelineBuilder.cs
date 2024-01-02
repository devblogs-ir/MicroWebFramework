using MicroWebFramework.Models;
using MicroWebFramework.Pipes;

namespace MicroWebFramework.Builders;

/// <summary>
/// using builder pattern tp simplify creating objects 
/// </summary>
public class PipelineBuilder
{
    private List<Type> _pipes = new List<Type>();

    public PipelineBuilder AddPipe(Type pipe)
    {
        _pipes.Add(pipe);
        return this;

    }

    public PipelineBuilder AddPipe<TType>()
    {
        AddPipe(typeof(TType));
        return this;
    }

    public Action<HttpContext> Build()
    {
        var lastestIndex = _pipes.Count - 1;

        Pipe? lastHandler = (Pipe)Activator.CreateInstance(_pipes[lastestIndex], null); //new Action<HttpContext>((httpContext) => { })

        for (int i = lastestIndex - 1; i >= 0; i--)
        {
            Pipe? currentHandler = (Pipe)Activator.CreateInstance(_pipes[i], new[] { lastHandler.Handle });
            lastHandler = currentHandler;
        }

        return lastHandler.Handle;

    }
}
