namespace PipelineDesignPattern;

public static class Messages
{
    public static string AccessingFromBannedCountryException(string countryName)
    {
        return $"We're sorry! We can't service customers from {countryName}:(";
    }

    public static string InvalidUrlFormatException(string url)
    {
        return $"URL format mush be {{controller}}/{{action}}/{{parameter}}\nRequested URL: {url}";
    }

    public static string EndPointNotFoundException(string url)
    {
        return $"URL {url} not found!";
    }

    public static string CliOptionNotProvidedException(string optionName)
    {
        return $"Option -{optionName} not provided!";
    }
}

