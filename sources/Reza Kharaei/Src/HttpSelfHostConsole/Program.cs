using HttpSelfHostConsole.Framework;
using HttpSelfHostConsole.Framework.Helper;
using HttpSelfHostConsole.Framework.Models;
using HttpSelfHostConsole.Framework.Pipelines;
using Microsoft.Owin;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks; 
using System.Web.Http.SelfHost;

namespace HttpSelfHostConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:7777/";

            var config = new HttpSelfHostConfiguration(baseAddress); 
            config.MessageHandlers.Add(new CustomMessageHandler()); 
            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Server is running at: " + baseAddress);
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }                                 
        }

        public class CustomMessageHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var ipAddress = UrlHelper.GetClientIpAddress(request); 
                var httpContext = new HttpContext { IP = ipAddress, Url = request.RequestUri.AbsoluteUri };
                var pipeline = new PipelineBuilder()
                                                   .AddPipe(typeof(ExceptionHandlingPipe))
                                                   .AddPipe(typeof(AuthenticationPipe))
                                                   .AddPipe(typeof(EndPointPipe))
                                                   .Build(httpContext);


                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Check Console Application for result") };
                 
            }

            
        }
    } 
}
