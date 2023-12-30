﻿using MicroWebFramework.Presentation.Common;
using MicroWebFramework.Presentation.CustomExceptions;
using MicroWebFramework.Presentation.Pipeline;
using System.Reflection;

namespace MicroWebFramework.Presentation.Framework;
public class EndPointPipe : Pipe
{
    public EndPointPipe() : base() { }
    public EndPointPipe(Action<PipelineContext> next) : base(next) { }

    public override void Invoke(PipelineContext context)
    {
        (string controller, string method, string parameter) = Url.GetUrlPath(context.HttpContext.Request.Url.AbsoluteUri);

        var metaData = $"MicroWebFramework.Presentation.Controllers.{controller}Controller";
        string assemblyPath = Assembly.GetExecutingAssembly().Location;
        Type type = Assembly.LoadFrom(assemblyPath).GetType(metaData);

        if (!type.IsInstancable())
            throw new InvalidRequestException(context.HttpContext.Request.Url.AbsoluteUri);

        var controllerInstance = Activator.CreateInstance(type, new[] { context });
        MethodInfo action = type.GetMethod(method);
        if (action is null)
            throw new InvalidRequestException(context.HttpContext.Request.Url.AbsoluteUri);

        ParameterInfo[] parameters = action.GetParameters();

        action.Invoke(controllerInstance, parameters.Length > 0 ? new[] { Convert.ChangeType(parameter, parameters[0].ParameterType) } : null);

    }
}
