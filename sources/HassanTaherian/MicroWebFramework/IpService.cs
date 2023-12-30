namespace PipelineDesignPattern;

public class IpService : IIpService
{
    private readonly ICountryRepository countryRepository;
    private readonly IDictionary<int, Country> blackList;

    public IpService(ICountryRepository countryRepository)
    {
        this.countryRepository = countryRepository;
        blackList = GetBlackList();
    }

    private IDictionary<int, Country> GetBlackList()
    {
        var countries = countryRepository.FetchAll();
        return countries.Where(country => country.IsBanned)
                        .ToDictionary(country => country.Code);
    }

    public bool IsOriginFromBannedCountries(string ip)
    {
        int originIpCountryCode = GetOriginCountryCode(ip);
        return blackList.ContainsKey(originIpCountryCode);
    }

    public Country? GetOriginCountry(string ip)
    {
        var originIpCountryCode = GetOriginCountryCode(ip);
        blackList.TryGetValue(originIpCountryCode, out var originCountry);
        return originCountry;
    }

    private int GetOriginCountryCode(string ip)
    {
        return int.Parse(ip.Split('.')[0]);
    }
}