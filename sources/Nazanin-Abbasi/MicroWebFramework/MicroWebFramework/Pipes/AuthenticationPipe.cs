using Dumpify;
using MicroWebFramework.Models;

namespace MicroWebFramework.Pipes;

public class AuthenticationPipe(Action<HttpContext> next) : Pipe(next)
{
    public override void Handle(HttpContext httpContext)
    {
        "Start Authentication...".Dump("Authorising");

        if (httpContext is null)
            throw new IPNullException("IP is not provided.");

        if (httpContext.IP is "164.215.56.0")
            throw new InvalidIPException("You are not eligible to login.");

        "Eligible".Dump("Login successful!");

        if (_next is not null) _next(httpContext);

    }
}