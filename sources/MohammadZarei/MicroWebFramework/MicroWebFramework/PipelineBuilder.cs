using MicroWebFramework.Pipes;

namespace MicroWebFramework;

public class PipelineBuilder
{
    private readonly List<Type> _pipes = new List<Type>();

    public PipelineBuilder WithType(Type t)
    {
        _pipes.Add(t);
        return this;
    }

    public Action<Context> Build()
    {
        Action<Context> pipeline = null!;

        for (int i = _pipes.Count - 1; i >= 0; i--)
        {
            Type pipeType = _pipes[i];

            var pipeInstance = Activator.CreateInstance(pipeType, new object[] { pipeline });
            pipeline = ((Pipe)pipeInstance!).Handle;
        }

        return pipeline;
    }
}

