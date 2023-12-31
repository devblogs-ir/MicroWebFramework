using System.Text;

namespace MicroWebFramework;
public class ProductsController
{
    private readonly HttpContext _httpContext;

    public ProductsController(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }
    // Products/GetAllProducts
    public void GetAllProducts()
    {
        _httpContext.Response.OutputStream.Write(
                Encoding.UTF8.GetBytes("List of all products."));
    }
    List<User> users = new List<User>()
    {
        new User() { Id = 1, Name = "User1" },
        new User() { Id = 2, Name = "User2" },
        new User() { Id = 3, Name = "User3" },
        new User() { Id = 4, Name = "User4" },
        new User() { Id = 5, Name = "User5" },
    };

    // Products/GetUserById/{id}
    public void GetUserById(int id)
    {
        if (!users.Any(p => p.Id == id))
        {
            _httpContext.Response.OutputStream.Write(
                Encoding.UTF8.GetBytes($"No user was found with id: {id}!!"));
            return;
        }
        _httpContext.Response.OutputStream.Write(
                Encoding.UTF8.GetBytes(
                    users.SingleOrDefault(p => p.Id == id).Name));
        return;
    }
}
