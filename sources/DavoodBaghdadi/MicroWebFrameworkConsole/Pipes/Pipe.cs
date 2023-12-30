namespace MicroWebFrameworkConsole.Pipes
{
    public abstract class Pipe
    {
        public Func<HTTPContext,string> _next;

        public Pipe()
        {
            _next = null!;
        }
        public Pipe(Func<HTTPContext,string> next)
        {
            _next = next;
        }
        public abstract string Handle(HTTPContext context);
    }
}
