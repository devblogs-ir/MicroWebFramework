using System.Net.Http;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace PipelineDesignPattern
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

            String templateControllerName = $"PipelineDesignPattern.{controllerClass}Controller";

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
