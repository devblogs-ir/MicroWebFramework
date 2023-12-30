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
            Controllers.NonController nonController = new(httpContext);
            //Controller exception Handling
            var controllerClass = parts[1];
            if(controllerClass == string.Empty)
            {
                //Home
                controllerClass = "Home";
            }
            var templateControllerName = $"PipelineDesignPattern.Controllers.{controllerClass}Controller";           
            var typeController = Type.GetType(templateControllerName);
            if(typeController == null)
            {
                nonController.Exception();
                return;
            }
            //End of Controller exceptions

            //Method exception Handling
            string actionMethod = string.Empty;
            if (partsLength > 2) {
                actionMethod = parts[2];
            }  
            if(actionMethod == string.Empty)
            {
                actionMethod = "Index";
            }
            MethodInfo method = typeController.GetMethod(actionMethod);
            if(method == null)
            {
                nonController.Exception();
                return;
            }
            //End of Method exception Handling

            string methodParam = string.Empty;
            if (partsLength > 3 && parts[3] != string.Empty) {
                methodParam = parts[3];
            }
            var parametersInfo = method.GetParameters();

            var instance = Activator.CreateInstance(typeController, new[] { httpContext });
            if (methodParam != string.Empty)
            {
                var convertedParam = Convert.ChangeType(methodParam, parametersInfo[0].ParameterType);
                method.Invoke(instance, new[] { convertedParam });
            }
            else method.Invoke(instance, null);

            if (_next != null)
                _next(httpContext);
        }
    }
}
