using PipelineDesignPattern.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern.Pipes;
public class ExceptionHandlingPipe : Pipe
{
    public ExceptionHandlingPipe(Action<HttpContext> next)
    {
        _next = next;
    }
    private Action<HttpContext> _next;

    public override void Handle(HttpContext httpContext)
    {
        Console.WriteLine("starting except...");
        httpContext.Response += "starting except...";
        try
        {
            if (_next != null)
                _next(httpContext);
        }
        catch (Exception ex) when (ex is IPBanException)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }

    }
}
