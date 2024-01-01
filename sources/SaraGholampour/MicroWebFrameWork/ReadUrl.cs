namespace MicroWebFrameWork;

using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public interface LabelValue
{
    string label { get; set; }
    string value { get; set; }
}

public class ReadUrl
{
    public void SendOutPut(string output, HttpListener listener)
    {
        HttpListenerContext context = listener.GetContext();
        HttpListenerResponse response = context.Response;
        response.Headers.Set("Content-Type", "text/plain");

        string data = output;
        byte[] buffer = Encoding.UTF8.GetBytes(data);
        response.ContentLength64 = buffer.Length;

        Stream outputStream = response.OutputStream;
        outputStream.Write(buffer, 0, buffer.Length);
    }

    public static T ParseUrl<T>(string url)
        where T : new()
    {
        T instance = new T();
        Type instanceType = instance.GetType();
        PropertyInfo[] properties = instanceType.GetProperties();

        string[] splitedUrl = url.Split('?');

        if (splitedUrl.Length>1&& splitedUrl[1] is not null)
        {
            string query = splitedUrl[1];

            string[] splitedQuery = query.Split('&');
            Console.WriteLine("now lets compare query by instance");
            foreach (string filter in splitedQuery)
            {
                string[] keyValue = filter.Split('=');
                PropertyInfo findedPropertyFromInstance = properties.FirstOrDefault(
                    node => node.Name == keyValue[0]
                );
                if (findedPropertyFromInstance is not null)
                {
                    if (findedPropertyFromInstance.CanWrite)
                    {
                        if (findedPropertyFromInstance.PropertyType == typeof(string))
                        {
                            findedPropertyFromInstance.SetValue(instance, "sara");
                        }
                    }
                }
            }
        }

        return instance;
    }
}
