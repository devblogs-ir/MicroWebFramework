using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using System.Text;

namespace MicroWebFramework.Pipes;

public class EndpointHandler(Action<Context> next) : Pipe(next)
{
    public override void Handle(Context context)
    {
        var httpListenerContext = context.HttpListenerContext;

        var segments = UrlExtension.Splliter(context.HttpListenerContext.Request.Url!.ToString());

        var controllerClass = segments[0];
        var actionMethod = segments[1];

        var templateControllerName = $"{ApplicationConsts.SolutionName}.Controllers.{controllerClass}Controller";
        var typeController = Type.GetType(templateControllerName);
        MethodInfo method = typeController!.GetMethod(actionMethod)!;

        var parameterInfos = method!.GetParameters();

        object result;

        if (parameterInfos.Length is 0)
        {
            var instance = Activator.CreateInstance(typeController, new[] { context });

            result = method.Invoke(instance, null);
        }
        else
        {
            var userIdAsString = segments[2];

            var userIdAsInt = Convert.ChangeType(userIdAsString, parameterInfos[0].ParameterType);

            var instance = Activator.CreateInstance(typeController, new[] { context });

            result = method.Invoke(instance, new[] { userIdAsInt });
        }

        EditingResponse(httpListenerContext, result);

        if (next is not null)
            _next.Invoke(context);
    }

    private static void EditingResponse(HttpListenerContext httpListenerContext, object result)
    {
        var jsonResponse = JsonManager.ToJsonString(result);
        var responseStream = httpListenerContext.Response.OutputStream;

        httpListenerContext.Response.ContentType = "application/json";

        var responseBuffer = Encoding.UTF8.GetBytes(jsonResponse);
        responseStream.Write(responseBuffer, 0, responseBuffer.Length);

        responseStream.Close();
    }
}

public class JsonManager
{
    public static string ToJsonString(object obj) => JsonConvert.SerializeObject(obj);
}


public class UrlExtension
{
    public static string[] Splliter(string url) 
        => url.Replace(ApplicationConsts.BaseAddress, "").Split('/');
}
