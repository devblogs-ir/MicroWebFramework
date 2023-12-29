using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern
{
    public class PipeBuilder
    {
        private List<Type> pipes = new List<Type>();

        public PipeBuilder AddPipe<TType>()
        {
            pipes.Add(typeof(TType));
            return this;
        }
        
        public Action<HttpContext> Build()
        {
            Action<HttpContext> firstHandler = null;

            for (int i = pipes.Count - 1; i >= 0; i--)
            {
                var pipeType = pipes[i];
                
                var pipeInstance = (Pipe)Activator.CreateInstance(pipeType, firstHandler);

                firstHandler = pipeInstance.Handle;
            }

            return firstHandler;

           
        }

       

    }
}
