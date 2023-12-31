using Dumpify;

namespace MicroWebFrameWork.Pipes;

public class ExceptionHandlerPipe : BasePipe
{
    public ExceptionHandlerPipe(Action<HttpContext> next) : base(next)
    {
           
    }

    public override void HandlePipe(HttpContext context)
    {
        "Start ExceptionHandling pipe".Dump();

        try
        {
            if (_next is not null) _next(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        "End ExceptionHandling pipe".Dump();
    }
}
