using System.Net;
using System.Security.Authentication;
using MicroWebFramework.Http;
using MicroWebFramework.Mvc;

namespace MicroWebFramework.Middlewares;
public class ExceptionHandlingMiddleware : MiddlewareBase
{
    public ExceptionHandlingMiddleware() { }
    public ExceptionHandlingMiddleware(RequestDelegate? next) : base(next) { }
    public override async Task InvokeAsync(HttpListenerContext context)
    {
        try
        {
            if (Next is not null)
                await Next(context);
        }
        catch (AuthenticationException ex)
        {
            ResultGenerator.Unauthorized(context, ex.Message);
        }
        catch (EntryPointNotFoundException ex)
        {
            ResultGenerator.NotFound(context, ex.Message);
        }
        catch (Exception ex)
        {
            ResultGenerator.Error(context, ex.Message);
        }
    }
}