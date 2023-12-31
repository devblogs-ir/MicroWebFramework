using Dumpify;


public class ExceptionHandlingPipe : BasePipe
{
    public ExceptionHandlingPipe(Action<HttpContext> next) : base(next) { }

    public override void Invoke(HttpContext context)
    {
        "Start ExceptionHandling pipe".Dump();
        try
        {
            if (Next is not null) Next(context);
        }
        catch (Exception e)
        {
            e.Message.Dump();
        }

        "End ExceptionHandling pipe".Dump();
    }
}

