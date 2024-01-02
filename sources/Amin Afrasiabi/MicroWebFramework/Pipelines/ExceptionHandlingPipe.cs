using Dumpify;
using MicroWebFramework.Models;
using PipelineDesignPattern.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace PipelineDesignPattern.Pipelines;

public class ExceptionHandlingPipe : Pipe
{
    public ExceptionHandlingPipe() : base() { }
    public ExceptionHandlingPipe(Action<HttpContext> next) : base(next) { }

    public override void Handle(HttpContext context)
    {
        "Start ExceptionHandling...".Dump();
        try
        {
            ArgumentNullException.ThrowIfNull(context);

            if (_next is not null) _next(context);

            "Done".Dump("\n");
        }
        catch (InvalidUserInputException ex)
        {
            ex.Message.Dump("ERROR!");
            HandleException(context, HttpStatusCode.NotFound, "404! Not Found");
        }
        catch (IranianIPBlockedException ex)
        {
            ex.Message.Dump("ERROR!");
            HandleException(context, HttpStatusCode.Forbidden, "Invalid IP Address");
        }
        catch (WrongUrlException ex)
        {
            ex.Message.Dump("ERROR!");
            HandleException(context, HttpStatusCode.NotFound, "404! Not Found");
        }
        catch (ArgumentNullException ex)
        {
            ex.Message.Dump("ERROR!");
            HandleException(context, HttpStatusCode.BadRequest, "UnExpected Error");
        }
    }
    private void HandleException(HttpContext context, HttpStatusCode httpStatusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpStatusCode;
        var data = JsonSerializer.Serialize(new BaseResult
        {
            Code = (int)httpStatusCode,
            Message = message
        });

        var buffer = Encoding.UTF8.GetBytes(data);
        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        context.Response.Close();
    }
}
