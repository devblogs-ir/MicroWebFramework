namespace PipelineDesignPattern.Pipelines.Builder;

public class PipelineBuilder
{
    private readonly List<Type> _pipes = [];

    public PipelineBuilder AddPipe(Type pipe)
    {
        if (!_pipes.Contains(pipe))
        {
            _pipes.Add(pipe);
        }

        return this;
    }

    public PipelineBuilder AddPipe<TType>() where TType : Pipe
    {
        AddPipe(typeof(TType));
        return this;
    }

    public Action<HttpContext> Build()
    {
        var lastIndex = _pipes.Count - 1;

        var selectedPipe = _pipes.ElementAtOrDefault(lastIndex);
        ArgumentNullException.ThrowIfNull(selectedPipe);

        var selectedHandler = Activator.CreateInstance(selectedPipe, null!) as Pipe;

        if (_pipes.Count == 1)
        {
            return selectedHandler.Handle;
        }

        for (int i = lastIndex - 1; i > 0; i--)
        {
            selectedPipe = _pipes.ElementAtOrDefault(i);

            selectedHandler = Activator.CreateInstance(selectedPipe!, new[] { selectedHandler.Handle }) as Pipe;
        }

        var firstHandler = Activator.CreateInstance(_pipes.First(), new[] { selectedHandler.Handle }) as Pipe;

        return firstHandler.Handle;
    }
}