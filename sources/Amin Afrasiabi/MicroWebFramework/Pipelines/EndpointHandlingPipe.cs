using PipelineDesignPattern.Exceptions;

namespace PipelineDesignPattern.Pipelines;

public class EndpointHandlingPipe : Pipe
{
    public EndpointHandlingPipe() : base() { }
    public EndpointHandlingPipe(Action<HttpContext> next) : base(next) { }

    public override void Handle(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var parts = context.Url.Split(separator: '/');

        var controllerClass = parts.ElementAtOrDefault(1);
        var actionMethods = parts.ElementAtOrDefault(2);
        var userInput = parts.ElementAtOrDefault(3);

        var templateControllerName = $"PipelineDesignPattern.Controllers.{controllerClass}Controller";

        var controllerType = Type.GetType(templateControllerName);

        if (controllerType is null)
            throw new WrongUrlException(controllerClass);

        var methodInfo = controllerType.GetMethod(actionMethods ?? "");

        if (methodInfo == null)
            throw new WrongUrlException(NotFoundPart: actionMethods);

        var methodParameters = methodInfo.GetParameters().ToList();

        object? methodInput = null;
        if (methodParameters.Count != 0)
        {
            try
            {
                methodInput = Convert.ChangeType(userInput, methodParameters[0].ParameterType);
            }
            catch (Exception)
            {
                throw new InvalidUserInputException(message: "Invalid Input!");
            }
        }

        var instance = Activator.CreateInstance(controllerType, [context]);

        object?[]? parameters = methodParameters.Count != 0 ? new[] { methodInput } : null;
        methodInfo.Invoke(instance, parameters);

        if (_next is not null) _next(context);
    }
}