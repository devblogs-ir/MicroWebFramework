using MicroWebFramework.Models;

namespace MicroWebFramework.PipLines
{
    public class PipeLineBuilder
    {
        protected List<Type> _types = new();
        public PipeLineBuilder AddPipe<TType>()
        {
            _types.Add(typeof(TType));
            return this;
        }
        public Pipe Build()
        {
            var latestIndex = _types.Count - 1;
            var selectedPipe = Activator.CreateInstance(_types[latestIndex], null) as Pipe;

            for (int index = latestIndex - 1; index > 0; index--)
                selectedPipe = Activator.CreateInstance(_types[index], new Action<HttpContext>[] { selectedPipe.Handle }) as Pipe;

            var firstPipe = Activator.CreateInstance(_types[0], new Action<HttpContext>[] { selectedPipe.Handle }) as Pipe;

            return firstPipe;
        }
    }
}
