using Dumpify;
using MicroWebFramework.Presentation.Common;
using MicroWebFramework.Presentation.CustomExceptions;
using MicroWebFramework.Presentation.Models;
using MicroWebFramework.Presentation.Pipeline;
using System.Net;

namespace MicroWebFramework.Presentation.Framework;
public class ExceptionHandlingPipe : Pipe
{
    public ExceptionHandlingPipe(Action<PipelineContext> next) : base(next)
    {
    }
    public override void Invoke(PipelineContext context)
    {
        try
        {
            "starting exception handling".Dump();
            Next(context);
        }
        catch (InvalidIpAddressException ex) { "Invalid ip address ".Dump(ex.Message); HandleException(context, HttpStatusCode.Unauthorized); }
        catch (InaccessibilityException ex) { "Invalid request from ".Dump(ex.Message); HandleException(context, HttpStatusCode.Unauthorized); }
        catch (UnknownIpAddressException ex) { "Ip address is unknown ".Dump(ex.Message); HandleException(context, HttpStatusCode.Unauthorized); }
        catch (InvalidRequestException ex) { "Url is not valid ".Dump(ex.Message); HandleException(context, HttpStatusCode.NotFound); }
        catch (NotImplementedPipelineException) { "No pipes have been added to the pipeline".Dump(); HandleException(context, HttpStatusCode.BadGateway); }
        catch (Exception ex) { "exception occurod".Dump(ex.Message); HandleException(context, HttpStatusCode.InternalServerError); }
    }
    private void HandleException(PipelineContext context, HttpStatusCode httpStatusCode)
    {
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)httpStatusCode;
        var buffer = context.HttpContext.Response.GetBytes(new Result<object>
        {
            IsSuccess = false,
            StatusCode = (int)httpStatusCode,
            Message = "an error has occurred"
        });
        context.HttpContext.Response.OutputStream.Write(buffer, 0, buffer.Length);
        context.HttpContext.Response.Close();
    }
}
