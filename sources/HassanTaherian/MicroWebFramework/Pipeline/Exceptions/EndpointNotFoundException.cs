namespace MicroWebFramework;
public class EndPointNotFoundException(string url) : PipelineException(Messages.EndPointNotFoundException(url))
{
}