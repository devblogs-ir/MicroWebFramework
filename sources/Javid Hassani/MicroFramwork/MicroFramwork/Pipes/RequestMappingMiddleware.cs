using Dumpify;
using MicroFramwork.Common;
using Newtonsoft.Json;
using System.Reflection;

namespace MicroFramwork.Pipes;

public class RequestMappingMiddleware(Action<ApplicationContext> next) : Middleware(next)
{
    public override void Handle(ApplicationContext context)
    {
        "start mapping".Dump();
        var route = context.Request.Url!.SplitRoute(context.ApplicationBase);

        var type = route.Controller.GetFromAssembly();

        var instance = Activator.CreateInstance(type);

        MethodInfo methodInfo = type.GetMethod(route.Method)!;
        var methodParameters = methodInfo.GetParameters();

        object response = new();
        if (methodParameters.Length > 0)
            response = methodInfo.InvokeWithParameters(instance!, route.Query);
        else
            response = methodInfo.Invoke(instance, null)!;

        var responseString = JsonConvert.SerializeObject(response);

        context.Response.WriteResponse(responseString);

        _next?.Invoke(context);

        "end mapping".Dump();
    }
}
