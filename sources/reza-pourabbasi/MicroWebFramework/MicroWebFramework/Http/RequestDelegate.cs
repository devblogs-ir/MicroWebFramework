using System.Net;

namespace MicroWebFramework.Http;
public delegate Task RequestDelegate(HttpListenerContext context);