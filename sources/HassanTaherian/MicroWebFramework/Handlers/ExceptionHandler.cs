using Dumpify;

namespace PipelineDesignPattern;
public class ExceptionHandler : BaseHandler
{
    public override void Handle(HttpContext httpContext)
    {
        try
        {
            next?.Invoke(httpContext);
        }
        catch (ApplicationException ex)
        {
            ex.Message.Dump("!!!Application Error!!!");
        }
        finally
        {
            "End Exception Handling".Dump();
        }
    }
}
