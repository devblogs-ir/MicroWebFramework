using System.Net;
using System.Reflection;
namespace MicroWebFrameWork;


public interface IPipe<T>
{
    void Handle(T param);
}

public abstract class Pipe(Action<HttpContext> next) : IPipe<HttpContext>
{
    public Action<HttpContext> _next = next;
    public abstract void Handle(HttpContext httpContext);
}

public class ExceptionHandling(Action<HttpContext> next) : Pipe(next)
{
    public override void Handle(HttpContext httpContext)
    {
        try
        {
            if (_next is not null)
                _next(httpContext);
        }
        catch (NotProvidedIpException e)
        {
            Console.WriteLine(e);
        }
    }
}

public class Authentication(Action<HttpContext> next) : Pipe(next)
{
    public override void Handle(HttpContext httpContext)
    {
        if (next is null)
            throw new NotProvidedIpException(httpContext.Ip);
        else if (httpContext.Ip is "85.198.20.134")
            throw new InvalidException();
        if (_next is not null)
            _next(httpContext);
    }
}

public class EndPointHandling(Action<HttpContext> next) : Pipe(next)
{
    public override void Handle(HttpContext httpContext)
    {
        var splitedUrl = httpContext.Url.Split("/");
        var controllerName = splitedUrl[1];
        var actionName = splitedUrl[2];
        var controllerAssembly = $"PipeLineDesignPattern.{controllerName}Controller";
        try
        {
            var controllerType = Type.GetType(controllerAssembly);
            if (controllerType is null)
                throw new Alert("controller not found !");

            if (actionName is null)
                throw new Alert("action method not found !");

            Console.WriteLine(controllerType);
            var controllerInstance = Activator.CreateInstance(
                controllerType,
                new[] { httpContext }
            );
            MethodInfo actionMethod = controllerType.GetMethod(actionName);
            ProductsController filters = ReadUrl.ParseUrl<ProductsController>(httpContext.Url);

            actionMethod.Invoke(controllerInstance, new[] { httpContext });
        }
        catch (Alert ex)
        {
            Console.WriteLine(ex.Message);
        }

        if (_next is not null)
            _next(httpContext);
    }
}
