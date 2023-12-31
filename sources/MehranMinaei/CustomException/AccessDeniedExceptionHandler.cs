namespace CustomException;

public class AccessDeniedException : Exception
{
    public AccessDeniedException():base() {

    }

    public AccessDeniedException(string msg) : base(msg) {
        Console.WriteLine(msg);
    }

    public AccessDeniedException(string msg, Exception innerException) : base(msg, innerException) {
        Console.WriteLine($"{msg} {innerException.Message}");
    }
}