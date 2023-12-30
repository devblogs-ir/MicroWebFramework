namespace PipelineDesignPattern;
public class EndPointNotFoundException(string url) : ApplicationException(Messages.EndPointNotFoundException(url))
{
}