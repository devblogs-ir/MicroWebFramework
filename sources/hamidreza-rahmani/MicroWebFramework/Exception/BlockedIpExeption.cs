namespace MicroWebFramework;

public class BlockedIpExeption : Exception
{
    public BlockedIpExeption()
    {
    }

    public BlockedIpExeption(string message) : base(message)
    {
    }

    public BlockedIpExeption(string message, Exception innerException) : base(message, innerException)
    {
    }
}