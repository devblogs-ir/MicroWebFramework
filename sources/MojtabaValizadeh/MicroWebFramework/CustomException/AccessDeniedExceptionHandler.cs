namespace MicroWebFramework;

public class AccessDeniedExceptionHandler: Exception
{
    public AccessDeniedExceptionHandler() : base()
    {
    }

    public AccessDeniedExceptionHandler(string message) : base(message)
    {
    }

    public AccessDeniedExceptionHandler(string message, Exception innerException) : base(message, innerException)
    {
    }
}