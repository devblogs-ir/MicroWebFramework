using Dumpify;
using MicroWebFramework;

var url = "http://localhost:9083";
IUiAdapter uiAdapter = new HttpAdapter(url);
IPipelineDirector pipelineDirector = new PipelineDirector();

while (true)
{
    var request = await uiAdapter.GetRequestAsync();

    if (request is not null)
    {
        var context = pipelineDirector.Process(request);
        uiAdapter.SendResponse(context);
    }
}