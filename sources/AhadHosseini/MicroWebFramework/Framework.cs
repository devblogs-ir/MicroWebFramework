using Dumpify;
using MicroWebFramework.Models;
using System;
using System.Reflection;

namespace MicroWebFramework;

public class Framework
{
    public void Authentication(HttpContext httpContext, Action<HttpContext> action)
    {
        "star Authentication".Dump();
        try
        {
            if (httpContext.Request.RemoteEndPoint.ToString().StartsWith("188"))
                throw new Exception($"The {httpContext.Request.RemoteEndPoint.ToString()} Invalid Ip");
            else
                action(httpContext);

        }
        catch (Exception ex)
        {
            ex.Message.Dump();
        }
        "End Authentication".Dump();
    }

    public void ExceptionHandling(HttpContext httpContext, Action<HttpContext> action)
    {
        "starting ExceptionHandling".Dump();
        try
        {
            action(httpContext);
        }
        catch (Exception ex)
        {
            ex.Message.Dump();
        }
        "End ExceptionHandling".Dump();
    }

    public void EndPointHandling(HttpContext httpContext, Action<HttpContext> action)
    {
        var uri = httpContext.Request.Url;
        string UrlNotBaseurl = uri.PathAndQuery; //Not Base Url
        var UrlArray = UrlNotBaseurl.Split("/"); //Array of Url
        string ControllerName = UrlArray[1];
        string ActionName = UrlArray[2];
        var IdValue = UrlArray[3];

        var ControllerFullName = $"{typeof(Framework).Namespace}.Controller.{ControllerName}Controller" ;
        var ControllerType = Type.GetType(ControllerFullName);   

        MethodInfo method = ControllerType.GetMethod(ActionName);

        var ParametersInfo = method.GetParameters();
        var UserIdInt = Convert.ChangeType(IdValue , ParametersInfo[0].ParameterType);

        var instans = Activator.CreateInstance(ControllerType, new[] { httpContext });
        method.Invoke(instans , new[] { UserIdInt });






        "End Point".Dump();
       
    }
}
