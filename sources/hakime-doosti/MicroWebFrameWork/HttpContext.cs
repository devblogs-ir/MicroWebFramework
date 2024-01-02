using System.Net;

namespace MicroWebFrameWork
{
    public class HttpContext
    {
        public string IP { get; set; }

        /// <summary>
        /// ControllerName/ActionName
        /// </summary>
        public string Url { get; set; }

        public HttpListenerContext ListenerContext { get; set; }
    }
}
