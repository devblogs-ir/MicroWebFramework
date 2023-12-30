using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Context
{
    public class HttpContext
    {
        public string IP { get; set; } = null!;
        public string Url { get; set; } = null!;
        public HttpListenerRequest Request { get; set; }
        public object Response { get; set;}
    }
}
