namespace PipelineDesignPattern.Exceptions;
public class IPBanException : Exception
{
    private const string ExceptionMessage = "Your IP `{0}`  has been blocked.";
    public IPBanException(string ip) : base(message: string.Format(ExceptionMessage, ip)) { }
}