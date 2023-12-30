using Dumpify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.PipeLineConsoleApplication
{
    public class FrameWork
    {
        public delegate void Action(HttpContext httpContext);
        public class ExceptionHandelingPipe(Action<HttpContext> next) : Pipe(next)
        {
            public override void Handle(HttpContext httpContext)
            {
                try
                {
                    next(httpContext);
                }
                catch (Exception ex)
                {

                    ex.Message.Dump();
                }
            }
        }
        public class AuthenticationPipe(Action<HttpContext> next) : Pipe(next)
        {
            public override void Handle(HttpContext httpContext)
            {
                "Start Authentication".Dump();
                if (httpContext.Ip == "89.199.162.192") throw new Exception("You Are From Iran");
                next(httpContext);
                "End Authentication".Dump();
            }
        }

        public class EndpointHandlingPipe : Pipe
        {

            public EndpointHandlingPipe() : base()
            {

            }
            public EndpointHandlingPipe(Action<HttpContext> next) : base(next)
            {

            }

            public override void Handle(HttpContext httpContext)
            {
                var urlParts = httpContext.Url.Split('/');
                var methodName = "";
                var controllerName = "";
                var UserId = "";
                try
                {
                    UserId = urlParts[5];
                    methodName = urlParts[4];
                    controllerName = urlParts[3];
                }
                catch
                {
                    throw new Exception(" Url Not Valid ");
                }

                var templateControllerName = $"MicroWebFramework.PipeLineConsoleApplication.{controllerName}Controller";
                var typeController = Type.GetType(templateControllerName) ?? throw new Exception(" type Not Found ");

                var instanceController = Activator
                    .CreateInstance(typeController, new[] { httpContext }) ?? throw new Exception(" Controller Not Found ");

                MethodInfo method = typeController.GetMethod(methodName) ?? throw new Exception(" method Not Found ");
                var parameters = method.GetParameters();

                var userIdAsInt = Convert.ChangeType(
                    UserId,
                    parameters[0].ParameterType);

                method.Invoke(instanceController, new[] { userIdAsInt });
            }
        }

        public void CORS(HttpContext httpContext, Action<HttpContext> action)
        {
            action(httpContext);
            "End Cors".Dump();
        }
        public void Routing(HttpContext httpContext, Action<HttpContext> action)
        {
            "Routing".Dump();
            action(httpContext);
        }

    }
}

