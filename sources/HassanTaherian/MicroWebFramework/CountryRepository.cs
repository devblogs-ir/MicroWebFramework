namespace PipelineDesignPattern;
public class CountryRepository : ICountryRepository
{
    // Assume that this data will genertate by some sort of Data Access layer
    public IEnumerable<Country> FetchAll()
    {
        return [
            new() { Name = "Iran", Code = 83, IsBanned = true },
            new() { Name = "Russia", Code = 31, IsBanned = true },
            new() { Name = "US", Code = 64, IsBanned = false },
            new() { Name = "Finland", Code = 86, IsBanned = false },
            new() { Name = "Sudan", Code = 43, IsBanned = true },
            new() { Name = "North Korea", Code = 13, IsBanned = true }
                ];
    }
}

