using MicroWebFrameWork.Pipes;

namespace MicroWebFrameWork;

public class PipeBuilder
{
    private List<Type> _pipes = [];

    public PipeBuilder AddPipe(Type type)
    {
        _pipes.Add(type);
        return this;
    }

    public PipeBuilder AddPipe<TType>()
    {
        AddPipe(typeof(TType));
        return this;
    }

    public Action<HttpContext> Build()
    {
        var lastIndex = _pipes.Count - 1;

        var selectedPipe = Activator.CreateInstance(_pipes[lastIndex], null) as BasePipe;

        for (int i = lastIndex - 1; i > 0; i--)
        {
            selectedPipe = Activator.CreateInstance(_pipes[i], new[] { selectedPipe.HandlePipe }) as BasePipe;
        }

        var firstPipe = Activator.CreateInstance(_pipes[0], new[] { selectedPipe.HandlePipe }) as BasePipe;

        return firstPipe.HandlePipe;
    }
}
