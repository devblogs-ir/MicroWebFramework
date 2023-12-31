using System.Net;
using System.Reflection;

namespace MicroWebFrameWork
{
    public class PipelineBuilder (HttpContext context)
    {
        private List<Type> _pipes = new List<Type>();
        private HttpContext _context= context;

        public PipelineBuilder  AddPipe(Type pipe)
        {
            _pipes.Add(pipe);
            return this;
        }
       
        public PipelineBuilder Build(Action<HttpContext> action)
        {
            for (int i = _pipes.Count - 1; i >= 0; i--)
            {
                Type pipeType = _pipes[i];

                var pipe = Activator.CreateInstance(pipeType, 
                    new object[] { action });

                MethodInfo method = pipeType.GetMethod("Handel");
                try { method.Invoke(pipe, new object[] { _context }); }
                catch(Exception e) {  }
               
            }
            action.Invoke(_context);
             return this;
        }
     
    }
}
