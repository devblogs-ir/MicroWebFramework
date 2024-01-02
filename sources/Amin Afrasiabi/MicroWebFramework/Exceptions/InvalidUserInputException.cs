namespace PipelineDesignPattern.Exceptions;
public class InvalidUserInputException : Exception
{
    public InvalidUserInputException() { }
    public InvalidUserInputException(string message) : base(message) { }
    public InvalidUserInputException(string message, Exception inner) : base(message, inner) { }
}
