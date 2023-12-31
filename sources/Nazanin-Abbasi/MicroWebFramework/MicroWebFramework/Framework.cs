using Dumpify;
using MicroWebFramework.Models;
using System.Reflection;

namespace MicroWebFramework;

/// <summary>
/// Pipeline methods are defined here.
/// </summary>
public class Framework
{
    public void Authentication(HttpContext httpContext, Action<HttpContext> action)
    {
        "Start Authentication".Dump("Authorising");

        if (httpContext.IP is "164.215.56.0")
        {
            throw new InvalidIPException("You are not eligible to login.");
        }

        "Eligible".Dump("Login successful!");

        action(httpContext);

    }


    public void ExceptionHandling(HttpContext httpContext, Action<HttpContext> action)
    {
        try
        {
            "Start ExceptionHandling".Dump("ExceptionHandling");

            action(httpContext);
        }
        catch (InvalidIPException e)
        {
            e.Message.Dump();
        }
        catch (Exception ex)
        {
            ex.Message.Dump();
        }
    }

    /// <summary>
    /// The purpose of this method is to handle incoming HTTP requests and route them to the appropriate controller 
    /// and action method based on the URL provided in the httpContext parameter.
    /// </summary>
    public void EndpointHandling(HttpContext httpContext, Action<HttpContext> action)
    {
        try
        {
            var parts = httpContext.Url.Split('/');

            var controllerClass = parts[1];

            var actionMethod = parts[2];

            var templateControllerName = $"PipelineDesignPattern.{controllerClass}Controller";

            var typeController = Type.GetType(templateControllerName);

            //create an instance in runtime dynamically
            var instance = Activator.CreateInstance(typeController, new[] { httpContext });

            MethodInfo method = typeController.GetMethod(actionMethod);

            method.Invoke(instance, null);
        }
        catch (Exception ex)
        {
            ex.Message.Dump();
        }
    }
}
