namespace MicroWebFramework;

public class AccessingFromBannedCountryException(string countryName) : PipelineException(Messages.AccessingFromBannedCountryException(countryName))
{
}