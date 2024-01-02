namespace Pipeline;

public class PipelineBuilder
{
    private List<Type> _pipe = new List<Type>() ;

    public PipelineBuilder AddPipe(Type pipe)
    {
        _pipe.Add(pipe);
        return this;
    }

    public PipelineBuilder AddPipe<TType>()
    {
       return AddPipe(typeof(TType));
        
    }

    public Action<HttpContext> Build()
    { 
        if (_pipe.Count == 0) throw new Exception();
         var currentPipe= (Pipe) Activator.CreateInstance(_pipe[^1], null)!;
       for (var i = _pipe.Count()-1; i > 0; i--)
        {
            currentPipe = (Pipe) Activator.CreateInstance(_pipe[i-1], currentPipe.Handle)!;

        }

       return currentPipe.Handle;

    }

}