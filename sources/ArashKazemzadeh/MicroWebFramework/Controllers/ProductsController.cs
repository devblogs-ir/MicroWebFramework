using MicroWebFramework.Models;

namespace MicroWebFramework.Controllers;
public class ProductsController
{
    private HttpContext _context;
    public ProductsController(HttpContext context)
    {
        _context = context;
    }
    public void GetAll(HttpContext httpContext)
    {
        Console.WriteLine("all products");
    }
    public void GetById(int id)
    {
        Console.WriteLine($"Products=> id : {id}");
    }
}