namespace MicroWebFramework;
public class CliOptionNotProvidedException(string optionName) : Exception(Messages.CliOptionNotProvidedException(optionName))
{
}
