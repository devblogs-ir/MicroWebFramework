using Dumpify;
using MicroFramwork.Exceptions;

namespace MicroFramwork.Pipes;

public class ExceptionHandlerMiddleware(Action<ApplicationContext> next) : Middleware(next)
{
    public override void Handle(ApplicationContext context)
    {
        try
        {
            "ExceptionHandling Started".Dump();
            _next.Invoke(context);
            "ExceptionHandling End".Dump();
        }
        catch (ForbiddenAccessException ex)
        {
            $"Error 403 : {ex.Message}".Dump();
        }
        catch (NotFoundException ex)
        {
            $"Error 404 : {ex.Message}".Dump();
        }
    }
}
