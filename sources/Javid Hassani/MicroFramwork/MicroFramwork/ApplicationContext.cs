using MicroFramwork.Common;
using System.Net;

namespace MicroFramwork;

public class ApplicationContext
{
    public ApplicationContext(HttpListenerContext context, ApplicationBase appBase)
    {
        Context = context;
        Request = context.Request;
        Response = context.Response;
        ApplicationBase = appBase;
    }
    public ApplicationBase ApplicationBase { get; init; }
    public HttpListenerContext Context { get; init; }
    public HttpListenerRequest Request { get; init; }
    public HttpListenerResponse Response { get; init; }
}