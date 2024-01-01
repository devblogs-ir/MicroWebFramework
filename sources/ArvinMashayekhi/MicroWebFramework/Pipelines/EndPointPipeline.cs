using Dumpify;
using MicroWebFramework.Common;
using MicroWebFramework.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Pipelines;
public class EndPointPipeline : BasePipe
{
    public EndPointPipeline(Action<HttpContext> context) : base(context)
    {
    }

    public override void Handler(HttpContext httpContext)
    {
        try
        {
            var parts = httpContext.Url.Split('/');
            var controllerName = parts[1];
            var actionName = parts[2];

            if (string.IsNullOrEmpty(controllerName))
                throw new ControllerExeptionHandler();

            if (string.IsNullOrEmpty(actionName))
                throw new ActionExeptionHandler();

            var controllerNameTemplate = $"MicroWebFramework.{controllerName}Controller";
            var controllerType = Type.GetType(controllerNameTemplate);
            var controllerInstance = Activator.CreateInstance(controllerType);
            var ActionInfo = controllerType.GetMethod(actionName);
            var parameterList = ActionInfo.GetParameters();
            int partLenghtMinSize = 3;
            int parameterListMinSize = 0;

            if(parameterList.Length > parameterListMinSize && parts.Length > partLenghtMinSize)
            {
                ActionInfo.Invoke(controllerInstance, parameterList);

            }
            else
            {
                ActionInfo.Invoke(controllerInstance, null);

            }
        }
        catch (IPExeptionHandler ex)
        {
            ex.Message.Dump();
        }
        catch (ActionExeptionHandler ex)
        {
            ex.Message.Dump();
        }
        catch (ControllerExeptionHandler ex)
        {
            ex.Message.Dump();
        }
    }
}

