using MicroWebFrameworkConsole;
using MicroWebFrameworkConsole.Pipes;


namespace PipelineDesignPattern.Pipes
{
    public class AuthenticationPipe : Pipe
    {
        public AuthenticationPipe()
        {
            _next = null!;
        }
        public AuthenticationPipe(Func<HTTPContext,string> next) : base(next)
        {

        }
        public override string Handle(HTTPContext hTTPContext)
        {

            if (_next is not null)
            {
                return _next(hTTPContext);
            }
            return string.Format("Starting Authentication..." + "\n" + "Authentication Operation ends here!");

        }
    }
}
