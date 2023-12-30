using System.Net;
using System.Reflection;
using MicroWebFramework.Http;

namespace MicroWebFramework.Middlewares;
public class EndPointMiddleware : MiddlewareBase
{
    public EndPointMiddleware() { }
    public EndPointMiddleware(RequestDelegate? next) : base(next) { }
    public override Task InvokeAsync(HttpListenerContext context)
    {
        var path = context?.Request?.Url?.LocalPath.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (path?.Length < 2)
            throw new EntryPointNotFoundException();

        var controllerName = $"MicroWebFramework.Controllers.{path?[0]}Controller";
        var actionName = path?[1];
        var controllerType = Type.GetType(controllerName, throwOnError: false, ignoreCase: true)!;
        var controllerInstance = Activator.CreateInstance(controllerType);
        var method = controllerType.GetMethod(actionName!, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        ParameterInfo[] parameters = method?.GetParameters()!;

        if (controllerInstance is null || method is null || (parameters.Length > 0 && path?.Length < 3))
            throw new EntryPointNotFoundException();

        PropertyInfo property = controllerType?.GetProperty("HttpContext")!;
        if (property is not null && property.CanWrite)
        {
            property.SetValue(controllerInstance, context);
        }

        method?.Invoke(controllerInstance, parameters.Length > 0 ?
                new[] { Convert.ChangeType(path?[2], parameters[0].ParameterType) } :
                null);

        return Task.CompletedTask;
    }
    private static bool IsSynchronousMethod(MethodInfo method)
    {
        return !typeof(Task).IsAssignableFrom(method.ReturnType);
    }
}
