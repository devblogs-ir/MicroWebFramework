namespace PipelineDesignPattern.Exceptions;
public class CliOptionNotProvidedException(string optionName) : Exception(Messages.CliOptionNotProvidedException(optionName))
{
}
