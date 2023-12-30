using System.Reflection;

namespace PipelineDesignPattern;
public class EndPointHandler : BaseHandler
{
    public override void Handle(HttpContext context)
    {
        var endPoint = context.Request.EndPoint;

        if (endPoint is null)
        {
            return;
        }

        var type = Assembly.GetExecutingAssembly().GetType($"PipelineDesignPattern.{endPoint.ControllerName}Controller");

        if (type is null)
        {
            throw new EndPointNotFoundException(context.Request.Url);
        }

        var methodInfo = type.GetMethod(endPoint.ActionName) ?? throw new EndPointNotFoundException(context.Request.Url);

        if (methodInfo is null)
        {
            throw new EndPointNotFoundException(context.Request.Url);
        }

        var classInstance = Activator.CreateInstance(type);
        if (endPoint.Parameter is not null)
        {
            methodInfo.Invoke(classInstance, [context,  int.Parse(endPoint.Parameter)]);
        }
        else
        {
            methodInfo.Invoke(classInstance, [context]);
        }

    }
}
