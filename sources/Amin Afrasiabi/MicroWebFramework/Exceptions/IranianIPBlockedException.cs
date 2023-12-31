namespace PipelineDesignPattern.Exceptions;
public class IranianIPBlockedException : Exception
{
    public IranianIPBlockedException() { }
    public IranianIPBlockedException(string message) : base(message) { }
    public IranianIPBlockedException(string message, Exception inner) : base(message, inner) { }
}
