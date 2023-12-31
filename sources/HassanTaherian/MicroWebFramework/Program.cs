using Dumpify;
using MicroWebFramework;

var url = "http://localhost:9083";
IUiAdapter uiAdapter = new HttpAdapter(url);

while (true)
{
    var request = await uiAdapter.GetRequestAsync();

    if (request is not null)
    {
        new PipelineDirector().Process(request);
    }
}