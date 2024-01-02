using Dumpify;
using MicroFramwork.Exceptions;

namespace MicroFramwork.Pipes;

public class AuthorizationMiddleware(Action<ApplicationContext> next) : Middleware(next)
{
    public override void Handle(ApplicationContext context)
    {
        "Authorization Started".Dump();
        if (context.Request.UserHostAddress.StartsWith("192.168"))
            throw new ForbiddenAccessException("Sorry You're Not Allowed to use this service");

        _next.Invoke(context);
        "Authorization End".Dump();
    }
}
