namespace MicroWebFrameworkConsole.Pipes
{
    public class PipeLineBuilder
    {
        List<Type> pipes = new List<Type>();


        public PipeLineBuilder AddPipe(Type pipeType)
        {
            pipes.Add(pipeType);
            return this;
        }
        public PipeLineBuilder AddPipe<T>()
        {
            pipes.Add(typeof(T));
            return this;
        }

        public Func<HTTPContext,string> Build()
        {
            var x = (Pipe)Activator.CreateInstance(pipes[2],null);
            var y = (Pipe)Activator.CreateInstance(pipes[1], new[] {x.Handle});
            var z = (Pipe)Activator.CreateInstance(pipes[0], new[] {y .Handle});
            return z.Handle;
        }

    }
}
