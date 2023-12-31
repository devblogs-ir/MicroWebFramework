using MicroWebFramework;

IUiAdapter uiAdapter = new CliAdapter(args);
var request = uiAdapter.GetRequest();

if (request is not null)
{
    new PipelineDirector().Process(request);
}