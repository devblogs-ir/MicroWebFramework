namespace PipelineDesignPattern.Exceptions;
public class IPBanException : Exception
{
    public IPBanException(string ip) : base(message: $"Your IP {ip}  has been blocked.") { }
}