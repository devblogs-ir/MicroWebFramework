using Dumpify;
using PipelineDesignPattern;

IUiAdapter uiAdapter = new CliAdapter(args);
var request = uiAdapter.GetRequest();

if (request is not null)
{
    new PipelineDirector().Process(request);
}