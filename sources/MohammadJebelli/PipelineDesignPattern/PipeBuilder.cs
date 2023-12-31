using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern;

public class PipeBuilder
{
    private List<Type> _pipes = [];

    public PipeBuilder AddPipe<TType>()
    {
        _pipes.Add(typeof(TType));
        return this;
    }

    public Action<HttpContext>? Build()
    {
        Action<HttpContext> firstHandler = null;

        for (int i = _pipes.Count - 1; i >= 0; i--)
        {
            var pipeType = _pipes[i];

            var pipeInstance = (Pipe)Activator.CreateInstance(pipeType, firstHandler);

            firstHandler = pipeInstance.Handle;
        }

        return firstHandler;


    }



}

