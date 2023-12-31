// See https://aka.ms/new-console-template for more information

using MicroWebFramework.CustomException;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace MicroWebFramework.Pipes
{
    public class EndpointPipe : Pipe
    {
        public EndpointPipe() : base()
        {
        }
        public EndpointPipe(Action<HttpListenerContext> next) : base(next)
        {

        }
        public override void Handle(HttpListenerContext httpListenerContext)
        {
            
            var parts = httpListenerContext.Request.Url.ToString().Split('/');
            if (parts.Length < 5)
            {
                throw new UrlException("The url is incorrect.");
            }

            var controllerClass = parts[3].Replace(parts[3].Substring(0,1), parts[3].Substring(0, 1).ToUpper());
            var actionMethod = parts[4].Replace(parts[4].Substring(0, 1), parts[4].Substring(0, 1).ToUpper());

            var tempateControllerName = $"MicroWebFramework.Controller.{controllerClass}Controller";
            var typeController = Type.GetType(tempateControllerName);
            if (typeController is null)
            {
                throw new UrlException("The controller not fount.");
            }

            MethodInfo? method = typeController.GetMethod(actionMethod);
            if (method is null)
            {
                throw new UrlException("The action not found.");
            }

            var paramsOfUrl = new List<object>();
            var parameterInfo = method.GetParameters();
            if (parameterInfo.Length > 0)
            {
                int arrayLentgh = parts.Length - 5;
                if (arrayLentgh <= 0 || arrayLentgh != parameterInfo.Length)
                {
                    throw new UrlException("Parameters count mismach");
                }

                for (int i = 5; i < parts.Length; i++)
                {
                    try
                    {
                        paramsOfUrl.Add(Convert.ChangeType(parts[i], parameterInfo[i - 5].ParameterType));
                    }
                    catch
                    {
                        throw new UrlException($"Not found type of parameter : {parameterInfo[i - 5].Name}");
                    }
                }
            }

            var instance = Activator.CreateInstance(typeController);
            var response = method.Invoke(instance, parameterInfo is null ? null : paramsOfUrl.ToArray());
            var responseString = JsonSerializer.Serialize(response);
            var buffer = Encoding.UTF8.GetBytes(responseString);
            httpListenerContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
            if (_next is not null)
                _next(httpListenerContext);
        }
    }
}