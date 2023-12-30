namespace PipelineDesignPattern;

public class AccessingFromBannedCountryException(string countryName) : ApplicationException(Messages.AccessingFromBannedCountryException(countryName))
{
}