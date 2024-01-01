using MicroFramwork.Exceptions;
using System.Reflection;
using System.Text;

namespace MicroFramwork.Common;

public static class RequestExtension
{
    private const int UrlWithQueryStringPartsCount = 2;

    public static bool DoesHaveQueryString(this string[] url)
    {
        if (url.Length == UrlWithQueryStringPartsCount)
            return true;

        return false;
    }

    public static string ReadRequestBody(this Stream stream)
    {
        using (StreamReader reader = new(stream))
        {
            return reader.ReadToEnd();
        }
    }
    public static object InvokeWithParameters(this MethodInfo method, object instance, string inputParams)
    {
        var parameters = method.GetParameters();

        if (string.IsNullOrEmpty(inputParams))
            throw new InvalidBindingException(@$"invalid input for method {method.Name},
                                                 this method contains this paramters 
                                                 {parameters.ExtractParameters()}");

        var queryValues = inputParams.ExtractQueryString();

        object[] finalParameters = new object[queryValues.Length];

        for (var i = 0; i < parameters.Length; i++)
        {
            finalParameters[i] = Convert.ChangeType(queryValues[i], parameters[i].ParameterType);
        }
        var result = method.Invoke(instance, finalParameters);

        return result!;
    }

    public static (string Controller, string Method, string Query) SplitRoute(this Uri baseUrl, ApplicationBase appBase)
    {
        string url = baseUrl.ToString();
        url = url.Remove(0, appBase.BaseUrl.Length);

        var fullRoute = url.Split('?');
        string query = string.Empty;
        if (fullRoute.DoesHaveQueryString())
        {
            query = fullRoute[1];
        }

        var internalRoute = fullRoute[0].Split('/');

        var controller = internalRoute[0];
        var method = internalRoute[1];
        return (controller, method, query);
    }
    public static string[] ExtractQueryString(this string input)
    {
        var parts = input.Split('&');
        string[] result = new string[parts.Length];

        for (int i = 0; i < parts.Length; i++)
        {
            int equalsIndex = parts[i].IndexOf('=');
            if (equalsIndex != -1 && equalsIndex < parts[i].Length - 1)
            {
                string value = parts[i].Split('=')[1];
                result[i] = value;
            }
        }
        return result;
    }
    public static string ExtractParameters(this ParameterInfo[]? parameters)
    {
        StringBuilder sb = new();

        foreach (var parameter in parameters!)
            sb.Append($"{parameter.Name} of type {parameter.ParameterType}");

        return sb.ToString();
    }
}
