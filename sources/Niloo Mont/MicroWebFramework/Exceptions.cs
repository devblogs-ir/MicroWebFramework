using System.Net;

namespace MicroWebFramework;
public class BannedIPException : ApplicationException
{
    public BannedIPException(string ip) :
        base($"{ip} is banned!")
    {
    }
}
public class NoControllerProvidedException : ApplicationException
{
    public NoControllerProvidedException() :
        base($"No controller is provided for this route.")
    {
    }
}

public class NoActionProvidedException : ApplicationException
{
    public NoActionProvidedException() :
        base($"No action is provided for this route.")
    {
    }
}

public class RouteNotFoundException : ApplicationException
{
    public RouteNotFoundException() :
        base($"Route not found!")
    {
    }
}

public class NoParameterProvidedException : ApplicationException
{
    public NoParameterProvidedException() :
        base($"No parameter is provided for this route.")
    {
    }
}

public class MyHttpRequestException : HttpListenerException
{
    public MyHttpRequestException() :
       base()
    {
    }
}