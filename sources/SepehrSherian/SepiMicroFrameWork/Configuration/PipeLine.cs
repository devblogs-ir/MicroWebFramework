using System.Net;
using System.Reflection;

namespace SepiMicroFrameWork.Configuration
{

    public abstract class Pipe
    {
        public Pipe()
        {
            
        }
        public Pipe(Action<HttpListenerContext> next)
        {
            _next = next;
        }
        protected Action<HttpListenerContext> _next;

        public abstract void Handler(HttpListenerContext Context);

    }

    public class ExeptionHandler(Action<HttpListenerContext> next) : Pipe(next)
    {
        public override void Handler(HttpListenerContext Context)
        {
            try
            {
                _next(Context);
            }
            catch (Exception ex)
            {
                var perfix = "http://localhost:9001";
                var message = ResponseConfig.ErrorMessage(ex.Message, perfix);
                Context.Response.OutputStream.Write(message, 0, message.Length);
            }
        }
    }

    public class Authentication(Action<HttpListenerContext> next) : Pipe(next)
    {
        public override void Handler(HttpListenerContext Context)
        {
            if (Context is null)
                throw new Exception(message: "Request Not Valid");;

            if (_next is not null) _next(Context);
        }
    }

    public class EndPointHandler : Pipe
    {
        public override void Handler(HttpListenerContext Context)
        {
            var parts = Context.Request.Url.OriginalString.Split("/");

            var ControllerClass = parts[3];
            var actionMethod = parts[4];
            var parameter = parts[5];

            var controllerName = $"SepiMicroFrameWork.Controller.{ControllerClass}";

            var typeController = Type.GetType(controllerName);

            MethodInfo method = typeController.GetMethod(actionMethod);

            if (!string.IsNullOrEmpty(parameter))
            {
                var parameterInfo = method.GetParameters();
                var convertedParameter = Convert.ChangeType(parameter, parameterInfo[0].ParameterType);
                var instance = Activator.CreateInstance(typeController, new[] { Context });
                method.Invoke(instance, new[] { convertedParameter });
            }

            else
            {
                var instance = Activator.CreateInstance(typeController, new[] { Context });
                method.Invoke(instance,null);
            }
          
            if (_next is not null) _next(Context);
        }
    }

}
