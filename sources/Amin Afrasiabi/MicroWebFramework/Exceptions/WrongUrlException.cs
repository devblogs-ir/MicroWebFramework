namespace PipelineDesignPattern.Exceptions;
public class WrongUrlException : Exception
{
    public WrongUrlException() { }
    public WrongUrlException(string? NotFoundPart)
        : base(string.IsNullOrWhiteSpace(NotFoundPart) ? "Oops! Url Not Found." : $"Oops Wrong Url! {NotFoundPart} Not Found.") { }
    public WrongUrlException(string message, Exception inner) : base(message, inner) { }

}
