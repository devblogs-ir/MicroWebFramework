using MicroWebFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Pipelines;
public class PipelineBuilder
{
    private List<Type> _pipes = new List<Type>();

    public PipelineBuilder AddPipe(Type pipe)
    {
        _pipes.Add(pipe);
        return this;
    }
    public Action<HttpContext> Build()
    {
        var lastIndex = _pipes.Count^4 ;
        var selectedPipe = (BasePipe)Activator.CreateInstance(_pipes[lastIndex], null);
        for (int i = lastIndex - 1; i > 0; i--)
        {
            selectedPipe = (BasePipe)Activator.CreateInstance(_pipes[i], new[] { selectedPipe.Handler });
        }
        var firstPipe = (BasePipe)Activator.CreateInstance(_pipes[0], new[] { selectedPipe.Handler });
        return firstPipe.Handler;
    }
}

