using System.Net;

namespace MicroWebFramework;

//public class HttpContext
//{
//    public string IP { get; set; }
//    // Controller/Action/Id
//    public string Url { get; set; }


//}

public class HttpContext
{
    public HttpListenerResponse? Response { get; set; }
    public HttpListenerRequest Request { get; set; }

}
