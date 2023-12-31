using System.Net;
using System.Text;
using System.Text.Json;
using Application.Models;

namespace Controller;
public abstract class ControllerBase
{
     public HttpListenerContext HttpContext { get; set; }
    protected ControllerBase(HttpListenerContext httpContext)
    {
        HttpContext = httpContext;
    }
     private static string Serialize(object? value)
    {
        return JsonSerializer.Serialize(value);
    }
    private static byte[] GetBytes(string value)
    {
        return Encoding.UTF8.GetBytes(value);
    }
    public void Ok(object value)
    {
        HttpContext.Response.ContentType = "application/json";
        HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        var result =GetBytes(Serialize(value));
        HttpContext.Response.OutputStream.Write(result, 0, result.Length);
        HttpContext.Response.OutputStream.Close();
    }
}