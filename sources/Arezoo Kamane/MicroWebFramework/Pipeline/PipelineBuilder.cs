
public class PipelineBuilder
{
    private List<Type> _pipes = new ();

    public PipelineBuilder UsePipe(Type pipe)
    {
        _pipes.Add(pipe);
        return this;
    }

    public Action<HttpContext> Build()
    {
        var pipeline = new List<BasePipe>();

        foreach (var pipeType in _pipes)
        {
            var instance = (BasePipe)Activator.CreateInstance(pipeType, new Action<HttpContext>(context => { }));

            pipeline.Add(instance);
        }

        for (int i = 0; i < pipeline.Count - 1; i++)
        {
            pipeline[i].Next = pipeline[i + 1].Invoke;
        }

        return pipeline.First().Invoke;
    }

}

