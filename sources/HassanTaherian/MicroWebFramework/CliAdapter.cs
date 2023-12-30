using Dumpify;
using PipelineDesignPattern.Exceptions;

namespace PipelineDesignPattern;
public class CliAdapter : IUiAdapter
{
    private readonly IList<CliOption> options;

    public CliAdapter(string[] args)
    {
        options = ExtractOptions(args);
    }

    private IList<CliOption> ExtractOptions(string[] args)
    {
        var options = new List<CliOption>();

        for (int i = 0; i < args.Length; i += 2)
        {
            if (args[i].StartsWith("-"))
            {
                CliOption option = new(args[i][1..], args[i + 1]);
                options.Add(option);
            }
        }
        return options;
    }

    public HttpContext? GetRequest()
    {
        try
        {
            ValidateOptions();
        }
        catch (CliOptionNotProvidedException exception)
        {
            exception.Message.Dump("UI Error");
            return null;
        }

        var ip = GetCliOption("ip");
        var url = GetCliOption("url");

        HttpContext request = new()
        {
            Id = 1,
            IpAdrress = ip.Value,
            Request = new()
            {
                Url = url.Value
            }
        };

        return request;
    }

    private void ValidateOptions()
    {
        var ip = GetCliOption("ip");

        if (ip is null)
        {
            throw new CliOptionNotProvidedException("ip");
        }

        var url = GetCliOption("url");

        if (url is null)
        {
            throw new CliOptionNotProvidedException("url");
        }
    }

    private CliOption? GetCliOption(string name)
    {
        return options.FirstOrDefault(option => option.Name == name);
    }
}
