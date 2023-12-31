using Dumpify;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace MicroWebFrameWork
{
    public abstract class Pipe(Action<HttpContext> next)
    {
        public Action<HttpContext> _next = next;

        public abstract void Handel(HttpContext httpContent);
         
    }

public class AuthenticationPip(Action<HttpContext> next) : Pipe(next)
{
    public override void Handel(HttpContext httpContext)
    {
        "begin AuthenticationPip".Dump();

        if (httpContext is null) 
            throw new CustomException("context is null ");

        if (httpContext.IP is null) 
            throw new CustomException("IP is null ");

        if (httpContext.IP is "192.15.0.0") 
            throw new CustomException("you are from Iran");

        if (_next is not null) _next(httpContext);
    }
}
public class ExceptionHandlingPip(Action<HttpContext> next) : Pipe(next)
{
    public override void Handel(HttpContext httpContext)
    {
             
        try
        {
                "begin ExceptionHandlingPip".Dump();
                 
                if (_next is not null) _next(httpContext);
        }
        catch (CustomException ex)
        {
                //ex.Message.Dump();
                byte[] buffer = Encoding.UTF8.GetBytes(ex.Message);
                httpContext.ListenerContext.Response.ContentLength64 = buffer.Length;
                httpContext.ListenerContext.Response.OutputStream.Write(buffer, 0, buffer.Length);

            }
        
    }

        
    }
public class EndpointPip(Action<HttpContext> next) : Pipe(next)
{

    public override void Handel(HttpContext httpContext)
    {
        "begin EndpointPip".Dump();
        var urlParts = httpContext.Url.Split('/');
        var methodName = "";
        var controllerName = "";
        var UserId = "";
        try
        {
            if(urlParts.Length is 6) 
                 UserId = urlParts[5];
            methodName = urlParts[4];
            controllerName = urlParts[3];
        }
        catch
        {
            throw new CustomException(" Url Not Valid ");
        }

        var templateControllerName = $"MicroWebFrameWork.{controllerName}Controller";

        var typeController = Type
            .GetType(templateControllerName) ??
            throw new CustomException(" type Not Found ");

        var instanceController = Activator
            .CreateInstance(typeController, new[] { httpContext }) ??
            throw new CustomException(" Controller Not Found ");

        MethodInfo method = typeController
            .GetMethod(methodName) ??
            throw new CustomException(" method Not Found ");

            if (urlParts.Length is 6)
            {
                var parameters = method.GetParameters();

                var userIdAsInt = Convert
                    .ChangeType(string.IsNullOrWhiteSpace(UserId) ? "0" : UserId,
                   parameters is not null ? parameters[0].ParameterType : null!);


                method.Invoke(instanceController, new[] { userIdAsInt });
            } 
            else
                method.Invoke(instanceController, null!);

            if (_next is not null) _next(httpContext);
    }
 }
}