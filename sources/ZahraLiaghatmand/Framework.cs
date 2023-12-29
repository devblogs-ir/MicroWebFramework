using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using PipelineDesignPattern.Exceptions;
using PipelineDesignPattern.Pipes;
namespace PipelineDesignPattern;
public class Framework
{
    public class PipelineBuilder
    {
        private List<Type> _pipes = new List<Type>();
        public PipelineBuilder AddPipe<TType>()
        {
            _pipes.Add(typeof(TType));
            return this;
        }
        public Action<HttpContext> build()
        {
            var currentPipe = (Pipe)Activator.CreateInstance(_pipes[_pipes.Count - 1], null);
            for (int index = _pipes.Count - 2; index >= 0; index--)
            {
                currentPipe = (Pipe)Activator.CreateInstance(_pipes[index], new[] { currentPipe.Handle });
            }
            return currentPipe.Handle;
        }

    }
}