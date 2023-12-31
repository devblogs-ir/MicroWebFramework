using Pipeline;
using System.Net;
using System.Text;


namespace Framework;

public class ExceptionHandler :Pipe{
    public ExceptionHandler(Action<HttpContext> next) : base(next)
    {
        Console.WriteLine("in Exception ");
        _next = next;
    }

    public ExceptionHandler()
    {
        _next = null;
    }
    public override void Handle(HttpContext httpContext)
    {
        try
        {
            _next(httpContext);
        }
        
        catch (Exception e)
        {
           
        }
    }

    protected HttpListenerResponse PrepareResponse<T>(T data, HttpListenerResponse response)
    {
        try
        {
           
           
            Console.WriteLine($"Response prepared for type '{typeof(T).FullName}'");

            return response;
        }
        finally
        {
            // Ensure the connection is closed
            response.Close();
        }
    }
    
}