using System.Reflection;

namespace MicroApiFramework
{
    public class Framework
    {
        public abstract class Pipe
        {
            public readonly Action<HttpContext> _next;
            public Pipe()
            {
                _next = null!;
            }
            public Pipe(Action<HttpContext> next)
            {
                _next = next;
            }
            public abstract void Handle(HttpContext httpContext);
        }

        public class ExceptionHandlingPipe : Pipe
        {
            public ExceptionHandlingPipe() : base() { }
            public ExceptionHandlingPipe(Action<HttpContext> next) : base(next) { }

            public override void Handle(HttpContext httpContext)
            {
                try
                {
                    httpContext.Response = "start ExceptionHandling";
                    if (_next is not null)
                    {
                        _next(httpContext);
                    }
                }
                catch (IPNotProvideException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public class AuthenticationPipe : Pipe
        {
            public AuthenticationPipe() : base() { }
            public AuthenticationPipe(Action<HttpContext> next) : base(next) { }

            public override void Handle(HttpContext httpContext)
            {
                httpContext.Response += "start Authentication";

                if (httpContext == null)
                {
                    throw new IPNotProvideException("IP is not provide");
                }
                else if (httpContext.IP is "37.255.130.05023")
                {
                    throw new InvalidIPException("you are from Iran");
                }

                if (_next is not null)
                {
                    _next(httpContext);
                }
            }
        }

        public class EndPointPipe : Pipe
        {
            public EndPointPipe() : base(){}
            public EndPointPipe(Action<HttpContext> next) : base(next) { }

            public override void Handle(HttpContext httpContext)
            {
                var parts = httpContext.Url.Split('/');
                var controllerName = parts[0];
                var actionName = parts[1];
                var userId = parts.Length==3? parts[2]:"0";

                var templateControllerName = $"MicroApiFramework.{controllerName}Controller";
                var typeController = Type.GetType(templateControllerName);
                MethodInfo method = typeController.GetMethod(actionName);

                var parameterInfos = method.GetParameters();
                var userIdAsInt = Convert.ChangeType(userId, parameterInfos[0].ParameterType);

                var instance = Activator.CreateInstance(typeController, new[] { httpContext });

                method.Invoke(instance, new[] { userIdAsInt });
                if (_next is not null)
                {
                    _next(httpContext);
                }
            }
        }

        public class PipelineBuilder
        {
            private List<Type> _pipes = new List<Type>();
            public PipelineBuilder AddPipe(Type pipe)
            {
                _pipes.Add(pipe);
                return this;
            }

            public PipelineBuilder AddPipe<TType>()
            {
                _pipes.Add(typeof(TType));
                return this;
            }

            public Pipe Build()
            {
                var latestIndex = _pipes.Count - 1;
                var selectedPipe = (Pipe)Activator.CreateInstance(_pipes[latestIndex], null);

                for (int i = latestIndex - 1; i > 0; i--)
                {
                    selectedPipe = (Pipe)Activator.CreateInstance(_pipes[i], new[] { selectedPipe.Handle });
                }

                var firstPipe = (Pipe)Activator.CreateInstance(_pipes[0], new[] { selectedPipe.Handle });
                return firstPipe;

            }
        }


    }

}
