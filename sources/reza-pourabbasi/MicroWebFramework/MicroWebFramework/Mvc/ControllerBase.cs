using System.Net;

namespace MicroWebFramework.Mvc;
public abstract class ControllerBase
{

    public HttpListenerContext HttpContext { get; set; }
    protected void Ok(object? value)
    {
        ResultGenerator.OK(HttpContext, value);
    }
    protected void NotFound(object? value)
    {
        ResultGenerator.NotFound(HttpContext, value);
    }
    protected void BadRequest(object? value)
    {
        ResultGenerator.BadRequest(HttpContext, value);
    }
    protected void Unauthorized(object? value)
    {
        ResultGenerator.Unauthorized(HttpContext, value);
    }
}