using System.Net;
using System.Security.Authentication;
using MicroWebFramework.Http;

namespace MicroWebFramework.Middlewares;
public class AuthenticationMiddleware : MiddlewareBase
{
    public AuthenticationMiddleware() { }
    public AuthenticationMiddleware(RequestDelegate? next) : base(next) { }
    public override async Task InvokeAsync(HttpListenerContext context)
    {
        var token = context.Request.Headers["Authorization"];
        if (token is null)
            throw new AuthenticationException("unauthorized");

        if (Next is not null)
            await Next(context);
    }
}
