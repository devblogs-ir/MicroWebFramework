using System.Reflection;
using MicroWebFramework.pipeline;

namespace MicroWebFramework.Middleware;

public class EndpointHandler : Pipe
{
    public EndpointHandler()
    {
        _next = null;
    }

    public override void Handle(HttpContext httpContext)
    {
        var path = httpContext?.Request?.Url?.LocalPath.Split('/', StringSplitOptions.RemoveEmptyEntries);

        if (path?.Length < 2)
            throw new EntryPointNotFoundException();

        var controllerName = $"MicroWebFramework.Controllers.{path?[0]}Controller";
        var actionName = path?[1];
        var controllerType = Type.GetType(controllerName, false, true)!;
        var controllerInstance = Activator.CreateInstance(controllerType);
        var method = controllerType.GetMethod(actionName!,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        var parameters = method?.GetParameters()!;

        if (controllerInstance is null || method is null || (parameters.Length > 0 && path?.Length < 3))
            throw new EntryPointNotFoundException();

        var property = controllerType?.GetProperty("HttpContext")!;
        if (property is not null && property.CanWrite) property.SetValue(controllerInstance, httpContext);

        method?.Invoke(controllerInstance,
            parameters.Length > 0 ? new[] { Convert.ChangeType(path?[2], parameters[0].ParameterType) } : null);

        httpContext.Response.Close();
    }
}