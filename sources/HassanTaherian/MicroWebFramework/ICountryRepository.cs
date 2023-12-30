namespace PipelineDesignPattern;

public interface ICountryRepository
{
    IEnumerable<Country> FetchAll();
}
