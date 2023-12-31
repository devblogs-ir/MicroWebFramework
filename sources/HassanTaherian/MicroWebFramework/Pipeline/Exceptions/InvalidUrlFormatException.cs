namespace MicroWebFramework;
public class InvalidUrlFormatException(string url) : PipelineException(Messages.InvalidUrlFormatException(url))
{
}