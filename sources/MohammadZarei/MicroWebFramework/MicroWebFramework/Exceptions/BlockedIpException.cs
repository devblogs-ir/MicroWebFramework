namespace MicroWebFramework.Exceptions;

public class BlockedIpException : Exception
{
    public BlockedIpException(string message) : base($"Your country Ip is Blocked. Your Ip is: {message}")
    {

    }
}

