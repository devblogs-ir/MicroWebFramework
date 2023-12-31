using Dumpify;
using System;

namespace MicroWebFramework;
public class CliAdapter : IUiAdapter
{
    private readonly IList<CliOption> options;
    private readonly IPipelineDirector _pipelineDirector;
    private readonly IDictionary<Guid, HttpContext> _unresponsedRequest;

    public CliAdapter(string[] args, IPipelineDirector pipelineDirector)
    {
        options = ExtractOptions(args);
        _pipelineDirector = pipelineDirector;
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

    public void SendResponse(HttpContext context)
    {
        context.Response.Message.Dump();
    }

    public Task<HttpContext?> GetRequestAsync()
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

        HttpContext request = new(ip?.Value, url?.Value);

        return Task.FromResult(request);
    }
}
