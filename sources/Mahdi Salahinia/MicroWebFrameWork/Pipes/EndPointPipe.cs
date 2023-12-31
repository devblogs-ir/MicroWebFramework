namespace MicroWebFrameWork.Pipes;

public class EndPointPipe : BasePipe
{
    public EndPointPipe() : base(null!)
    {

    }

    public EndPointPipe(Action<HttpContext> next) : base(next)
    {

    }

    public override void HandlePipe(HttpContext httpContext)
    {
        try
        {
            var urlParts = httpContext.Url.Split('/');

            var controllerName = urlParts[1];

            var actionName = urlParts[2];

            string urlOption;

            if (string.IsNullOrEmpty(controllerName) || string.IsNullOrEmpty(actionName))
                throw new Exception();

            var controllerNameTemplate = $"MicroWebFrameWork.Controllers.{controllerName}Controller";

            var controllerType = Type.GetType(controllerNameTemplate);
            
            var controllerInstance = Activator.CreateInstance(controllerType!, new[] { httpContext });

            var methodInfo = controllerType!.GetMethod(actionName) ?? throw new Exception();

            var parameterList = methodInfo.GetParameters();

            if (parameterList.Length > 0 && urlParts.Length > 3)
            {
                urlOption = urlParts[3];

                object[] parameters = new object[parameterList.Length];

                for (int i = 0; i < parameterList.Length; i++)
                {
                    var convertedParameter = Convert.ChangeType(urlOption, parameterList[i].ParameterType);

                    parameters[i] = convertedParameter;
                }

                methodInfo.Invoke(controllerInstance, parameters);
            }
            else
            {
                methodInfo.Invoke(controllerInstance, null);
            }

            if (_next is not null) _next(httpContext);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
