namespace CustomException;

public class NotFoundException : Exception
{
    public NotFoundException():base() {

    }

    public NotFoundException(string msg) : base(msg) {
        Console.WriteLine(msg);
    }

    public NotFoundException(string msg, Exception innerException) : base(msg, innerException) {
        Console.WriteLine($"{msg} {innerException.Message}");
    }
}