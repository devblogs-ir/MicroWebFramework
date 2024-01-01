using System.Reflection;
using MicroWebFramework.Models;
using MicroWebFramework.PipLines;

namespace MicroWebFramework.Middlewares
{
    public class EndPoint : Pipe
    {
        public EndPoint(Action<HttpContext> next) : base(next) { }
        public EndPoint() : base() { }
        public override void Handle(HttpContext httpContext)
        {
            Console.WriteLine("Starting EndPoint");

            string[] parts = httpContext.URL.Split('/');
            string controllerClass = parts[3];
            string actionMethod = parts[4];
            var queryString = parts[5];

            string templateControllerName = $"MicroWebFramework.Controllers.{controllerClass}Controller";

            Type controllerType = Type.GetType(templateControllerName);

            MethodInfo method = controllerType.GetMethod(actionMethod);

            ParameterInfo[] parameters = method.GetParameters();

            object queryStringNewType = Convert.ChangeType(queryString, parameters[0].ParameterType);

            object instance = Activator.CreateInstance(controllerType, new[] { httpContext });

            method.Invoke(instance, new[] { queryStringNewType });

            if (_next is not null)
                _next(httpContext);

            Console.WriteLine("Ending EndPoint");

        }
    }
}
