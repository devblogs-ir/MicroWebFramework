using System.Reflection;
using System.Text.Json;
using Dumpify;
using MicroWebFramework.Pipeline;

namespace MicroWebFramework.Pipes;
public class EndPointPipe : BasePipe
{
    public EndPointPipe(Action<HttpContext> next) : base(next) { }

    public override void Invoke(HttpContext context)
    {
        "Start EndPointHandling pipe".Dump();

        var requestUrl = context.Request.Url.LocalPath;

        var urlPart = requestUrl.Split("/");
        var controllerClass = urlPart[1];
        var actionMethod = urlPart[2];


        var entryAssembly = Assembly.GetEntryAssembly();
        var assembly = entryAssembly ?? Assembly.GetCallingAssembly();
        string assemblyNamespace = assembly.GetName().Name;

        var controllerTemplate = $"{assemblyNamespace}.{controllerClass}Controller";

        if (Type.GetType(controllerTemplate) is Type controllerType)
        {
            var controller = Activator.CreateInstance(controllerType, new[] { context });

            if (controllerType.GetMethod(actionMethod) is MethodInfo action)
            {
                var parameters = urlPart.Skip(3).ToArray();
                var paramTypes = action.GetParameters().Select(p => p.ParameterType).ToArray();

                object[] typedParameters = new object[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    typedParameters[i] = Convert.ChangeType(parameters[i], paramTypes[i]);
                }

                object returnValue = action.Invoke(controller, typedParameters);

                string jsonString = JsonSerializer.Serialize(returnValue);

                context.Response.ContentType = "application/json";
                using (var writer = new StreamWriter(context.Response.OutputStream))
                {
                    writer.Write(jsonString);
                }
            }

        }

        "End EndPointHandling pipe".Dump();
    }
}

