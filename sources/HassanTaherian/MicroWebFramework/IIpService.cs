namespace MicroWebFramework;

public interface IIpService
{
    bool IsOriginFromBannedCountries(string ip);
    public Country? GetOriginCountry(string ip);
}