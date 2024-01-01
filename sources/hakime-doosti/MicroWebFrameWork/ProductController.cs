using System.Text;

namespace MicroWebFrameWork
{
    public class ProductController(HttpContext httpContext)
    {
        public void GetAll()
        {
            if (httpContext.IP is null) 
                throw new CustomException(" IP Is null ");

            if (httpContext is null) 
                throw new CustomException(" httpContext Is null ");

            string result = $" return all user";

            byte[] buffer = Encoding.UTF8.GetBytes(result);
            httpContext.ListenerContext.Response.ContentLength64 = buffer.Length;
            httpContext.ListenerContext.Response.OutputStream.Write(buffer, 0, buffer.Length);

        }
        public void GetUserBuyId( int Id)
        {
            string result = $"user id :{Id}";

            byte[] buffer = Encoding.UTF8.GetBytes(result);
            httpContext.ListenerContext.Response.ContentLength64 = buffer.Length;
            httpContext.ListenerContext.Response.OutputStream.Write(buffer, 0, buffer.Length);

        }

    }
}
