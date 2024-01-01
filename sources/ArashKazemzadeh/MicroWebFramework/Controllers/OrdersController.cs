using MicroWebFramework.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroWebFramework.Controllers;

public class OrdersControllers
{
    private HttpContext _context;
    public OrdersControllers(HttpContext context)
    {
        _context = context;
    }
    public void GetAll(HttpContext httpContext)
    {
        Console.WriteLine("all Orders");
    }
    public void GetById(int id)
    {
        Console.WriteLine($"Orders=> id : {id}");
    }
}