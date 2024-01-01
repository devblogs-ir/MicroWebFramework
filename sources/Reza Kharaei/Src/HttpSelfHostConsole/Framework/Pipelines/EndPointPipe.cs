using HttpSelfHostConsole.Framework.Helper;
using HttpSelfHostConsole.Framework.Models;
using System;
using System.Reflection;

namespace HttpSelfHostConsole.Framework.Pipelines
{
    public class EndPointPipe : Pipe
    {
        public EndPointPipe() : base(null)
        {

        }

        public EndPointPipe(Action<HttpContext> next) : base(next)
        {

        }

        public override void Handler(HttpContext httpContext)
        {
            try
            {
                var request = UrlHelper.ParseUrl(httpContext.Url);

                var projectNamespace = Assembly.GetExecutingAssembly().GetName().Name;
                var controllerAssemblyName = $"{projectNamespace}.Controllers.{request.controllerName}Controller";

                var controllerType = Type.GetType(controllerAssemblyName);
                if (controllerType is null)
                    throw new Exception($"there is no controller named '{request.controllerName}'.");

                var method = controllerType.GetMethod(request.actionName);
                if (method is null)
                    throw new Exception($"there is no action named '{request.actionName}' in '{request.controllerName}' controller.");

                var controllerInstanse = Activator.CreateInstance(type: controllerType);

                if (request.parameterId.HasValue)
                {
                    var parameterInfo = method.GetParameters();
                    var intId = Convert.ChangeType(request.parameterId, parameterInfo[0].ParameterType);
                    method.Invoke(controllerInstanse, new[] { intId });
                }
                else
                    method.Invoke(controllerInstanse, null);


                if (_next != null)
                    _next(httpContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
