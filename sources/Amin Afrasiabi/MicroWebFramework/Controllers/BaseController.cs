using MicroWebFramework.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace PipelineDesignPattern.Controllers;

public abstract class BaseController
{
    private readonly HttpContext _context;

    public BaseController(HttpContext context)
    {
        _context = context;
    }
    public void Ok<TResult>(TResult? value)
    {
        _context.Response.ContentType = "application/json";
        _context.Response.StatusCode = (int)HttpStatusCode.OK;

        var result = JsonSerializer.Serialize(new BaseResult<TResult>()
        {
            Code = (int)HttpStatusCode.OK,
            Message = "Success",
            Result = value
        });

        var buffer = Encoding.UTF8.GetBytes(result);
        _context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        _context.Response.Close();
    }
}