using Dumpify;

namespace PipelineDesignPattern;
public class PipelineDirector : IPipelineDirector
{
    private PipelineBuilder pipelineBuilder { get; init; }
    public PipelineDirector()
    {
        pipelineBuilder = new();
        Build();
    }

    private void Build()
    {
        var countryRepository = new CountryRepository();
        var ipService = new IpService(countryRepository);

        RoutingHandler routingHandler = new();
        ExceptionHandler exceptionHandler = new();
        AuthorizationHandler authorizationHandler = new(ipService);
        EndPointHandler endPointHandler = new();

        pipelineBuilder.AddHandler(exceptionHandler)
                       .AddHandler(authorizationHandler)
                       .AddHandler(routingHandler)
                       .AddHandler(endPointHandler);
    }

    public void Process(HttpContext context)
    {
        $"Processing Request {context.Id}".Dump();

        pipelineBuilder.Run(context);

        $"End Process of Request {context.Id}".Dump();
        Console.WriteLine("\n");
    }
}
