using Pipeline;
using System.Net;
using System.Reflection;
namespace Framework;

public class EndpoindHandler : Pipe
{

    public EndpoindHandler(Action<HttpContext> next) : base(next)
    {
        _next = next;
    }

   public EndpoindHandler()
    {
        _next = null;
    }

    public override void Handle(HttpContext context)
    {
        Console.WriteLine("in the Endpoint " + context.Request.Url);
        var path = context?.Request?.Url?.LocalPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (path?.Length < 2)
            throw new EntryPointNotFoundException();
        var controllerName = $"{path?[0]}Controller";  
        var actionName = path?[1];
        var controllerType = Type.GetType(controllerName, throwOnError: false, ignoreCase: true)!;
        var controllerInstance = Activator.CreateInstance(controllerType);
        var method = controllerType.GetMethod(actionName!, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        ParameterInfo[] parameters = method?.GetParameters()!;  

        if (controllerInstance is null || method is null || (parameters.Length > 0 && path?.Length < 3))
            throw new EntryPointNotFoundException();
        
        PropertyInfo property = controllerType?.GetProperty("HttpContext")!;
        method?.Invoke(controllerInstance, parameters.Length > 0 ?
                new[] { Convert.ChangeType(path?[2], parameters[0].ParameterType) } :
                null);

       
        Console.WriteLine("in the Endpoint Path " + controllerType);  
        context.Response.Close();
    }
}
