using System.Net;

namespace SepiMicroFrameWork.Configuration;

public class PiplineBuilder
{
    public List<Type> _pipes = new List<Type>();

    public PiplineBuilder AddPipe<T>()
    {
        _pipes.Add(typeof(T));
        return this;
    }

    public Action<HttpListenerContext> Builder()
    {
        var lastIndex = _pipes.Count - 1;
        var seletedPipe = (Pipe)Activator.CreateInstance(_pipes[lastIndex], null);
        for(int item= lastIndex-1; item>0; item--)
        {
             seletedPipe = (Pipe)Activator.CreateInstance(_pipes[item], new[] { seletedPipe.Handler});
        }
        var firstPipe = (Pipe)Activator.CreateInstance(_pipes[0], new[] { seletedPipe.Handler });

        return firstPipe.Handler;
    }
}
