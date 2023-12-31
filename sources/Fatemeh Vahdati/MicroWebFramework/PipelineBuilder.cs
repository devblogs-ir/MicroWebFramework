// See https://aka.ms/new-console-template for more information

using MicroWebFramework.Pipes;
using System.Net;

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

    public Action<HttpListenerContext> Build()
    {
        var selectedHandle = (Pipe)Activator.CreateInstance(_pipes[_pipes.Count - 1], null);
        for (int i = _pipes.Count - 2; i > 0; i--)
        {
            selectedHandle = (Pipe)Activator.CreateInstance(_pipes[i], new[] { selectedHandle.Handle });
        }

        selectedHandle = (Pipe)Activator.CreateInstance(_pipes[0], selectedHandle.Handle);
        return selectedHandle.Handle;
    }
}