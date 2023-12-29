using System.Net;
using System.Text;
using Dumpify;
using Newtonsoft.Json;

namespace MicroWebFramework;

public class ExceptionHandlingMiddleWare:Pipe
{
    public ExceptionHandlingMiddleWare(Action<HttpContext> next) : base(next)
    {
        _next = next;
    }

    public override void Handle(HttpContext httpContext)
    {
        try
        {
            _next(httpContext);
        }
        catch (AccessDeniedExceptionHandler e)
        {
            PrepareResponse(e.Message, httpContext.Response);
        }
        catch (ExpiredException e)
        {
            PrepareResponse(e.Message, httpContext.Response);
        }
        catch (NotFoundException e)
        {
            PrepareResponse(e.Message, httpContext.Response);
        }
        catch (UnsupportedFormatException e)
        {
            PrepareResponse(e.Message, httpContext.Response);
        }
        catch (Exception e)
        {
            PrepareResponse(e.Message, httpContext.Response);
        }
    }

    protected HttpListenerResponse PrepareResponse<T>(T data, HttpListenerResponse response)
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(data);

            byte[] buffer = Encoding.UTF8.GetBytes(jsonData);

            response.ContentType = "application/json";
            response.ContentLength64 = buffer.Length;

            using (Stream output = response.OutputStream)
            {
                output.Write(buffer, 0, buffer.Length);
            }

            response.Close();

            Console.WriteLine($"Response prepared for type '{typeof(T).FullName}':\n{jsonData}");

            return response;
        }
        finally
        {
            // Ensure the connection is closed
            response.Close();
        }
    }
    

}