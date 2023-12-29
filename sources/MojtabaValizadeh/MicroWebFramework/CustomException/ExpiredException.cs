namespace MicroWebFramework;

public class ExpiredException : Exception
{
    public ExpiredException() : base()
    {
    }

    public ExpiredException(string message) : base(message)
    {
    }

    public ExpiredException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
    
