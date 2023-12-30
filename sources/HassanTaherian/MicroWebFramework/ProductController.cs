namespace PipelineDesignPattern;

using Dumpify;

public class ProductController
{
    public void GetUsers(HttpContext httpContext)
    {
        "All Users".Dump();
    }

    public void GetUserById(HttpContext httpContext, int id)
    {
        $"Single User {id}".Dump();
    }
}
