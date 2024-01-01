using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using MicroWebFramework.Models;

namespace MicroWebFramework;

public class PipeLineBuilder()
{
    private List<Type> _pipe = new ();

    public PipeLineBuilder AddPipe(Type pipe)
    {
        _pipe.Add(pipe);
        return this;
    }

    public PipeLineBuilder AddPipe<TType>()where TType : Pipe
    {
        AddPipe(typeof(TType));
        return this;
    }

    public Action<HttpContext> Build()
    {
        var pipes_ = (Pipe)Activator.CreateInstance(_pipe[^1], null);

        for (int i = _pipe.Count - 2; i >= 0; i--)
        {
            pipes_ = (Pipe)Activator.CreateInstance(_pipe[i], new[] { pipes_.Handle });
        }
        return pipes_.Handle;
    }

}
