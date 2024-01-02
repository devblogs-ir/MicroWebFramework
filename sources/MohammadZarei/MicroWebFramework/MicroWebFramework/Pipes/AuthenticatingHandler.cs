using Dumpify;
using MicroWebFramework.Exceptions;

namespace MicroWebFramework.Pipes;

public class AuthenticationHandler(Action<Context> next) : Pipe(next)
{
    public override void Handle(Context context)
    {
        var clientIp = context.HttpListenerContext.Request.RemoteEndPoint.Address.ToString();

        "Starting Authentication for user with IP: ".Dump($"{clientIp}");

        Console.WriteLine("Authenticating: Checking user credentials . . .");

        if (clientIp.ToString().Contains("iran"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Authentication failed for user with ip: {clientIp}!!!");
            Console.ResetColor();
            throw new BlockedIpException(clientIp);
        }

        if (context is not null)
            _next(context);

        "Ending Authentication for".Dump($"{clientIp}");
    }
}

