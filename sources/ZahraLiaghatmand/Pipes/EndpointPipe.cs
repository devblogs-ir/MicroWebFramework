using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern.Pipes
{
    public class EndPointPipe : Pipe
    {
        public EndPointPipe() : base() { }
        public EndPointPipe(HttpContent httpContent)
        {

        }
        public override void Handle(HttpContext httpContext)
        {
            var parts = httpContext.Url.Split('/');
            int partsLength = parts.Length;
            var controllerClass = parts[1];
            if (controllerClass == "favicon.ico") return;
            string methodParam = null;
            var actionMethod = parts[2];
            if (partsLength > 3 && parts[3] != string.Empty) {
                methodParam = parts[3];
            }
            
            var templateControllerName = $"PipelineDesignPattern.Controllers.{controllerClass}Controller";
            var typeController = Type.GetType(templateControllerName);
            MethodInfo method = typeController.GetMethod(actionMethod);
            var parametersInfo = method.GetParameters();
            var instance = Activator.CreateInstance(typeController, new[] { httpContext });
            if (methodParam is not null)
            {
                var userIdAsInt = Convert.ChangeType(methodParam, parametersInfo[0].ParameterType);
                method.Invoke(instance, new[] { userIdAsInt });
            }
            else method.Invoke(instance, null);

            if (_next != null)
                _next(httpContext);
        }
    }
}
