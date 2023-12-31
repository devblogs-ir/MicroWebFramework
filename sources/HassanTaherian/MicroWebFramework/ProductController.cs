namespace MicroWebFramework;
public class ProductController
{
    public void GetUsers(HttpContext httpContext)
    {
        httpContext.Response.Message = "All Users";
    }

    public void GetUserById(HttpContext httpContext, int id)
    {
        httpContext.Response.Message = $"User {id}";
    }
}
