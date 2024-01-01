using PipelineDesignPattern;

namespace MicroWebFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {

            HttpContext httpContext = new()
            { 
                IP = "185.185.20.177" ,
                URL= "https://faradars.org/Products/GetById/0"
            };
         

            ProductsController productsController = new(httpContext);

            //EndPoint endPoint = new(null); 
            //Authentication authentication = new(endPoint.Handle);
            //ExceptionHandling exceptionHandling = new(authentication.Handle);
            //exceptionHandling.Handle(httpContext);

            var pipLine = new PipeLineBuilder()
                .AddPipe<ExceptionHandling>()
                .AddPipe<Authentication>()
                .AddPipe<EndPoint>()
                .Build();

            pipLine.Handle(httpContext);
        }
    }
}
