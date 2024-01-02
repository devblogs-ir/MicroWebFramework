using System.Net;

namespace MicroFramwork.Common;

public static class ResponseExtension
{
    public static void WriteResponse(this HttpListenerResponse httpResponse, string responseBody)
    {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseBody);
        httpResponse.ContentLength64 = buffer.Length;
        var output = httpResponse.OutputStream;
        httpResponse.ContentType = "application/json";
        output.Write(buffer, 0, buffer.Length);

        httpResponse.Close();
    }
}
