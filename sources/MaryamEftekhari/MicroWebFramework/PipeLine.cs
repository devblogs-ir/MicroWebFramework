using MicroWebFramework.Context;
using System.Reflection;

namespace MicroWebFramework
{
    public abstract class Pipe
    {
        public Action<HttpContext> _next;
        public Pipe()
        {
            _next = null;
        }
        public Pipe(Action<HttpContext> next)
        {
            _next = next;
        }
        public abstract void Handle(HttpContext context);
    }

    public class AuthenticationPipe : Pipe
    {
        public AuthenticationPipe(Action<HttpContext> next) : base(next)
        {
        }

        public override void Handle(HttpContext context)
        {
            if (_next is not null) _next(context);
        }
    }

    public class ExceptionHandlingPipe : Pipe
    {
        public ExceptionHandlingPipe(Action<HttpContext> next) : base(next)
        {

        }

        public override void Handle(HttpContext context)
        {
            try
            {
                if (_next is not null)
                {
                    _next(context);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    public class EndPointPipe : Pipe
    {
        public EndPointPipe() : base() { }

        public EndPointPipe(Action<HttpContext> next) : base(next)
        {

        }

        public override void Handle(HttpContext context)
        {
            var endPoint = context.Url.Split("/");
            var controllerName = endPoint[3];
            var methodName = endPoint[4];
            var controllerType = Type.GetType($"MicroWebFramework.Controllers.{controllerName}Controller");
            MethodInfo method = controllerType.GetMethod(methodName);
            var controller = Activator.CreateInstance(controllerType);

            if (method != null)
            {
                object result = null;
                var paramsInfo = method.GetParameters();
                if (paramsInfo.Length > 0)
                {
                    var param = endPoint[5];
                    var userId = Convert.ChangeType(param, paramsInfo[0].ParameterType);
                    result = method.Invoke(controller, new[] { userId });
                }
                else
                {
                    result = method.Invoke(controller, null);
                }

                context.Response = result;
            }
        }
    }

    public class PipeLineBuilder
    {
        List<Type> pipes = new List<Type>();
        public PipeLineBuilder AddPipe(Type pipe)
        {
            pipes.Add(pipe);
            return this;
        }

        public Action<HttpContext> Build()
        {
            var index = pipes.Count - 1;
            var selectedPipe = (Pipe)Activator.CreateInstance(pipes[index], null);
            for (index -= 1; index >= 0; index--)
            {
                selectedPipe = (Pipe)Activator.CreateInstance(pipes[index], selectedPipe.Handle);
            }

            return selectedPipe.Handle;
        }
    }
}
