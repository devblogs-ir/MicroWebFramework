using System.Net;

namespace MicroWebFramework.Http;
public abstract class MiddlewareBase
{
    public RequestDelegate? Next { get; set; }
    public MiddlewareBase() { }
    public MiddlewareBase(RequestDelegate? next) => Next = next;
    public abstract Task InvokeAsync(HttpListenerContext context);
}