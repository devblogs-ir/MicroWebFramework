using HttpSelfHostConsole.Framework.Models;
using Microsoft.Owin;
using System.Net.Http;

namespace HttpSelfHostConsole.Framework.Helper
{
    public static class UrlHelper
    {
        private static int? TryParseNullable(string val)
        {
            int outValue;
            return int.TryParse(val, out outValue) ? (int?)outValue : null;
        }

        public static RoutingContext ParseUrl(string url)
        {
            var result = new RoutingContext();

            var SplitedUrl = url.Split('/');
            result.parameterId = TryParseNullable(SplitedUrl[SplitedUrl.Length - 1]);

            if (result.parameterId.HasValue)
            {
                result.controllerName = SplitedUrl[SplitedUrl.Length - 3];
                result.actionName = SplitedUrl[SplitedUrl.Length - 2];
            }
            else
            {
                result.controllerName = SplitedUrl[SplitedUrl.Length - 2];
                result.actionName = SplitedUrl[SplitedUrl.Length - 1];
            }

            return result;
        }

        public static string GetClientIpAddress(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                var owinContext = request.Properties["MS_OwinContext"] as OwinContext;
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }

            return null;
        }
    }
}
