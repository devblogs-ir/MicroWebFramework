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

    private int _controllerIndex = 1, _actionIndex=2, _lastUrlIndex=3;

    public override void Handle(HttpContext httpContext)
    {
        var splitted = httpContext.Url.Split('/');
        var partsCount = splitted.Length;

        if (partsCount < 3)
        {
            httpContext.HttpResponse += "";
            return;
        }
        var ControllerName = splitted[_controllerIndex].FirstLetterToUpper();
        var ActionName = splitted[_actionIndex].FirstLetterToUpper();
        var requestParameters = splitted.Skip(_lastUrlIndex).Take(partsCount - _lastUrlIndex);
        var templateControllerName = $"PipelineDesignPattern.{ControllerName}Controller";

        var type = Type.GetType(templateControllerName);

        if (type is null)
        {
            httpContext.HttpResponse += "Endpoint not found!";
            return;
        }

        var instance = Activator.CreateInstance(type, new[] { httpContext });
        var method = type.GetMethod(ActionName);

        if (method is null)
        {
            httpContext.HttpResponse += "Endpoint not found!";
            return;
        }
        var actionParams = method.GetParameters();

        if (actionParams is not null && actionParams.Length != requestParameters?.Count())
        {
            httpContext.HttpResponse += "Parameter mismatch";
            return;
        }

        var result = method.Invoke(instance, requestParameters.ToArray());
        httpContext.HttpResponse += result?.ToString();
    }
}

