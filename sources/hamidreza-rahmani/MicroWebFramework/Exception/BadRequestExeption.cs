namespace MicroWebFramework;

public class BadRequestExeption : Exception
{
    public BadRequestExeption()
    {
    }

    public BadRequestExeption(string message) : base(message)
    {
    }

    public BadRequestExeption(string message, Exception innerException) : base(message, innerException)
    {
    }
}