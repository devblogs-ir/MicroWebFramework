using System.Text;
using System.Text.Json;

namespace MicroWebFramework;
public class OrdersController
{
    private readonly HttpContext _httpContext;

    public OrdersController(HttpContext httpContext)
    {
        _httpContext = httpContext;
    }

    List<Order> orders = new List<Order>()
    {
        new Order() { Id = 1, Title = "Order1" },
        new Order() { Id = 2, Title = "Order2" },
        new Order() { Id = 3, Title = "Order3" },
        new Order() { Id = 4, Title = "Order4" },
        new Order() { Id = 5, Title = "Order5" },
    };

    // Orders/GetAllOrders
    public void GetAllOrders()
    {
        string ordersJson = JsonSerializer.Serialize(orders, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        _httpContext.Response.OutputStream.Write(
                Encoding.UTF8.GetBytes(ordersJson));
    }

    // Orders/GetOrderById/{id}
    public void GetOrderById(int id)
    {
        if (!orders.Any(p => p.Id == id))
        {
            _httpContext.Response.OutputStream.Write(
                Encoding.UTF8.GetBytes($"No order was found with id: {id}!"));
            return;
        }
        _httpContext.Response.OutputStream.Write(
                Encoding.UTF8.GetBytes(
                    orders.SingleOrDefault(p => p.Id == id).Title));
        return;
    }
}
