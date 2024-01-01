using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Common;
public class HttpContext
{
    public required string IP { get; set; }
    public required string Url { get; set; }
    public HttpListenerResponse Response { get; set; }
    public HttpListenerRequest Request { get; set; }


}

