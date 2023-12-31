using Dumpify;
using MicroWebFrameWork.Pipes;
using System.Net;
using System.Net.Http;

namespace MicroWebFrameWork;

public class PipeLineRunner
{
    public static void RunPipeLines(HttpListenerContext httpListenerContext)
    {
        try
        {
            HttpContext request = new()
            {
                IpAddress = httpListenerContext.Request.RemoteEndPoint?.Address.ToString(),
                Url = httpListenerContext.Request.RawUrl,
                Response = httpListenerContext.Response,
                Request = httpListenerContext.Request
            };

            var pipes = new PipeBuilder()
                .AddPipe<ExceptionHandlerPipe>()
                .AddPipe<AuthenticationPipe>()
                .AddPipe<EndPointPipe>()
                .Build();

            pipes(request);
        }
        catch (Exception ex)
        {
            $"Exception: {ex.Message}".Dump();
        }
        finally
        {
            httpListenerContext.Response.Close();
        }
    }
}
