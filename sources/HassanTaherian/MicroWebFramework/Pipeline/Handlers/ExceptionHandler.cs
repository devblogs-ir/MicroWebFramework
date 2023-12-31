﻿using Dumpify;

namespace MicroWebFramework;
public class ExceptionHandler : BaseHandler
{
    public override void Handle(HttpContext httpContext)
    {
        try
        {
            next?.Invoke(httpContext);
        }
        catch (PipelineException ex)
        {
            httpContext.Response.Message = ex.Message;  
            ex.Message.Dump("!!!Application Error!!!");
        }
        finally
        {
            "End Exception Handling".Dump();
        }
    }
}
