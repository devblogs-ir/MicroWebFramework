using MicroWebFramework.PipeLineConsoleApplication;
using static MicroWebFramework.PipeLineConsoleApplication.FrameWork;


FrameWork frameWork = new();
var httpcontext = new HttpContext() { UserId = 1 };
ProductsController productsController = new ProductsController(httpcontext);

HttpContext request1 = new()
{
    Ip = "102.15.0.0",
    Url = "https://localhost:5000/Products/GetUserById/1000"
};

HttpContext request2 = new()
{
    Ip = "192.15.0.0",
    Url = "https://localhost:5000/Orders/GetByUserId/3"
};


var firstHandler = new PipelineBuilder()
                    .AddPipe< ExceptionHandelingPipe>()
                    .AddPipe<AuthenticationPipe>()
                    .AddPipe<EndpointHandlingPipe>()
                    .Build();
firstHandler(request1);
firstHandler(request2);

