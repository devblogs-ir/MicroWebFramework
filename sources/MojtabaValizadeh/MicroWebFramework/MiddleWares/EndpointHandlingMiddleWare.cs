using System.Net;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace MicroWebFramework;

public class EndpointHandlingMiddleWare : Pipe
{
    public EndpointHandlingMiddleWare()
    {
        _next = null!;
    }

    public EndpointHandlingMiddleWare(Action<HttpContext> next) : base(next)
    {
    }

    public override void Handle(HttpContext httpContext)
    {
        var baseUrl = httpContext.Request.Url?.AbsolutePath.Split('?');

        var seprateUrl = baseUrl[0].Split('/');
        if (httpContext.Request.Url?.AbsolutePath is "/")
        {
            string responseString = "Hello, world!";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            httpContext.Response.ContentLength64 = buffer.Length;
            httpContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
            httpContext.Response.Close();
            return;
        }

        string namespaceName = "MicroWebFramework";

        // Get the current assembly (you may need to adjust this depending on your project structure)
        Assembly? currentAssembly = Assembly.GetEntryAssembly();

        // Get all types from the assembly that end with "Controller" and are derived from ControllerBase
        var controllerTypes = currentAssembly?.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.Name.EndsWith("Controller"));
        //Get Name of controller and action from url
        var controllerName = seprateUrl[^2];
        var actionName = seprateUrl[^1];
        //Get controller type base on name
        var controllerType = controllerTypes.Where(a => a.FullName.Contains(controllerName)).FirstOrDefault();

        if (controllerType is null)
        {
            throw new NotFoundException("Controller not found!");
        }

        var controllerInstance = Activator.CreateInstance(controllerType);
        MethodInfo method = controllerType.GetMethod(actionName);

        if (method is null)
        {
            throw new NotFoundException("Method not found!");
        }

        // Get the method parameters
        var parameters = method.GetParameters();

        // Prepare an array to hold parameter values
        object[] parameterValues = new object[parameters.Length];

        // Loop through each parameter and set its value
        for (int i = 0; i < parameters.Length; i++)
        {
            // You may need additional logic to extract values based on parameter names or types
            parameterValues[i] =
                GetParameterValueFromRequest(httpContext, parameters[i].Name, parameters[i].ParameterType);
        }


        try
        {
            var result = method.Invoke(controllerInstance, parameterValues);
            string jsonData = JsonConvert.SerializeObject(result);

            byte[] buffer = Encoding.UTF8.GetBytes(jsonData);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.ContentLength64 = buffer.Length;

            using (Stream output = httpContext.Response.OutputStream)
            {
                output.Write(buffer, 0, buffer.Length);
            }

            httpContext.Response.Close();
        }
        catch (Exception e)
        {
            httpContext.Response.Close();  
        }
        finally
        {
            // Ensure the connection is closed
            httpContext.Response.Close();
        }
        
    }

    private object GetParameterValueFromRequest(HttpContext httpContext, string paramName, Type paramType)
    {
        //In this method handle string and int parameter
        var paramValue = httpContext.Request.QueryString[paramName];
        if (paramType == typeof(int))
        {
            int intValue;
            if (int.TryParse(paramValue, out intValue))
            {
                return intValue;
            }

        }

        if (paramType == typeof(string))
        {
            return paramValue;
        }
        throw new UnsupportedFormatException($"Unsupported parameter type: {paramType}");

    }
}