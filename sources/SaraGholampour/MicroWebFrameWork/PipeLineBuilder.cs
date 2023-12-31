namespace MicroWebFrameWork;


public class PipeLineBuilder
{
    private List<Type> _pipes = new List<Type>();

    public PipeLineBuilder AddPipe(Type pipe)
    {
        _pipes.Add(pipe);
        return this;
    }

    public PipeLineBuilder Build(HttpContext httpContext)
    {
        Action<HttpContext> lastAction = null;


        foreach (var item in _pipes)
        {
            var instance = Activator.CreateInstance(item, new[] { lastAction }) as Pipe;
            lastAction = instance.Handle;
        }

        lastAction(httpContext);

        return this;
    }
}
