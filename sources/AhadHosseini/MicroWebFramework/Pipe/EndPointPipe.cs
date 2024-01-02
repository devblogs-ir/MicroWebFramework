using Dumpify;
using MicroWebFramework.Models;
using System.Reflection;


namespace MicroWebFramework;

public class EndPointPipe : Pipe
{
    public EndPointPipe() : base()
    {

    }
    public EndPointPipe(Action<HttpContext> next) : base(next)
    {

    }
    public override void Handle(HttpContext httpContext)
    {
        "staring End Point ".Dump();
        Uri uri = httpContext.Request.Url;
        string UrlNotBaseurl = uri.PathAndQuery; //Not Base Url
        var UrlArray = UrlNotBaseurl.Split("/"); //Array of Url
        string ControllerName = UrlArray[1];
        string ActionName = UrlArray[2];
        string IdValue = "";
        if (UrlArray.Length == 4) IdValue = UrlArray[3];

        var ControllerFullName = $"{typeof(EndPointPipe).Namespace}.Controllers.{ControllerName}Controller";
        var ControllerType = Type.GetType(ControllerFullName);

        MethodInfo method = ControllerType.GetMethod(ActionName);

        var instans = Activator.CreateInstance(ControllerType, new[] { httpContext });

        var ParametersInfo = method.GetParameters();

        if (ParametersInfo.Length > 0)
        {
            var UserIdInt = Convert.ChangeType(IdValue, ParametersInfo[0].ParameterType);
            method.Invoke(instans, new[] { UserIdInt });
        }
        else
            method.Invoke(instans, null);

        if (Next is not null) Next(httpContext);

        "End End Point".Dump();

    }
}
