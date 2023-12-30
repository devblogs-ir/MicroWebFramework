using System.Net;
using System.Text;
using System.Text.Json;

namespace MicroWebFramework.Mvc;
public static class ResultGenerator
{
    public static void OK(HttpListenerContext context, object? value)
    {
        WriteOutput(context, value, 200);
    }
    public static void NotFound(HttpListenerContext context, object? value)
    {
        WriteOutput(context, value, 404);
    }
    public static void BadRequest(HttpListenerContext context, object? value)
    {
        WriteOutput(context, value, 400);
    }
    public static void Unauthorized(HttpListenerContext context, object? value)
    {
        WriteOutput(context, value, 401);
    }
    public static void Error(HttpListenerContext context, object? value)
    {
        WriteOutput(context, value, 500);
    }
    private static string Serialize(object? value)
    {
        return JsonSerializer.Serialize(value);
    }
    private static byte[] GetBytes(string value)
    {
        return Encoding.UTF8.GetBytes(value);
    }
    private static void WriteOutput(HttpListenerContext context, object? value, int statusCode)
    {
        context.Response.StatusCode = statusCode;
        var result = GetBytes(Serialize(value));
        context.Response.OutputStream.Write(result, 0, result.Length);
        context.Response.OutputStream.Close();
    }
}