using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.PipeLineConsoleApplication
{
    public class PipelineBuilder
    {
        private List<Type> _pipes = new List<Type>();
        protected PipelineBuilder AddPipe(Type pipe)
        {
            _pipes.Add(pipe);
            return this;
        }
        public PipelineBuilder AddPipe<TType>()
        {
            AddPipe(typeof(TType));
            return this;
        }

        public Action<HttpContext> Build()
        {
            var latestIndex = _pipes.Count - 1;
            var selectedPipe = (Pipe)Activator.CreateInstance(_pipes[latestIndex], null);
            for (int index = latestIndex; index > 0; index--)
            {
                selectedPipe = (Pipe)Activator.CreateInstance(_pipes[index], new[] { selectedPipe.Handle });
            }
            var firstPipe = (Pipe)Activator.CreateInstance(_pipes[0], new[] { selectedPipe.Handle });
            return firstPipe.Handle;
        }

    }
}
