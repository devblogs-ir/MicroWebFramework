namespace MicroFramwork.Common;

public class ApplicationBase
{
    public string BaseUrl { get; private set; }

    public ApplicationBase(string baseUrl)
    {
        BaseUrl = baseUrl;
    }
}
