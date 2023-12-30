namespace PipelineDesignPattern
{
    public class Authentication : Pipe
    {
        public Authentication(Action<HttpContext> next) :base(next)
        {
        }
        public Authentication() : base() { }

        public override void Handle(HttpContext context)
        {
            Console.WriteLine("Starting Authentication");

            if (context is null)
                throw new Exception("ip is not provide");
            if (context.IP == "85.185.20.177")
                throw new Exception("invalid IP");

            if (_next is not null)
                _next(context);

            Console.WriteLine("Ending Authentication");

        }
    }
}
