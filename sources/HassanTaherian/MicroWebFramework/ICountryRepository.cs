namespace MicroWebFramework;

public interface ICountryRepository
{
    IEnumerable<Country> FetchAll();
}
