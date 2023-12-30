using Dumpify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern;

public class EndPointPipe : Pipe
{
    public EndPointPipe(Action<HttpContext> next) : base(next)
    {
    }

    public override void Handle(HttpContext httpContext)
    {
        var splitted = httpContext.Url.Split('/');

        var partsCount = splitted.Length;

        if (partsCount < 3)
        {
            httpContext.HttpResponse += "";
            return;
        }
        var ControllerName = splitted[1].FirstLetterToUpper();
        var ActionName = splitted[2].FirstLetterToUpper();

        var requestParameters = splitted.Skip(3).Take(partsCount - 3);

        var templateControllerName = $"PipelineDesignPattern.{ControllerName}Controller";

        var type = Type.GetType(templateControllerName);

        if (type is null)
        {
            httpContext.HttpResponse += "Endpoint not found!";
            return;
        }
        var instance = Activator.CreateInstance(type, new[] { httpContext });

        MethodInfo method = type.GetMethod(ActionName);

        var actionParams = method.GetParameters();

        if (actionParams is not null && actionParams.Length != requestParameters?.Count())
        {
            httpContext.HttpResponse += "Parameter mismatch";
            return;
        }
        var result = method.Invoke(instance, requestParameters.ToArray());

        httpContext.HttpResponse += result;
    }
}

