using System.Net;
using MicroWebFrameWork;


ReadUrl url = new();

var listener = new HttpListener();
listener.Prefixes.Add("http://localhost:8001/");

listener.Start();

Console.WriteLine("Listening on port 8001...");
HttpListenerContext context = listener.GetContext();
HttpListenerRequest req = context.Request;


string IpAddress = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
string UrlAddress = context.Request.Url.AbsolutePath.ToString();

HttpContext info = new()
{
    Ip = IpAddress,
    Url = UrlAddress
};

new PipeLineBuilder()
    .AddPipe(typeof(ExceptionHandling))
    .AddPipe(typeof(Authentication))
    .AddPipe(typeof(EndPointHandling))
    .Build(info);
