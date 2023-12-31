namespace MicroWebFramework;
public class InvalidUrlFormatException(string url) : ApplicationException(Messages.InvalidUrlFormatException(url))
{
}