namespace MicroWebFrameWork;

public class NotProvidedIpException:ApplicationException
{
    public NotProvidedIpException(string ip) : base($"{ip} not provided"){}

}
