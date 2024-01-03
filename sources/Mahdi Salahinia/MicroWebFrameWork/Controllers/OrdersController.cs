using MicroWebFrameWork.Models;

namespace MicroWebFrameWork.Controllers;

public class OrdersController
{
    private readonly List<Order> orders =
    [
        new() { Id = 1, Address = "Tehran", ProductDetail = "BMW 730li" },
        new() { Id = 2, Address = "Berlin", ProductDetail = "Benz CLS" },
        new() { Id = 3, Address = "Hamburg", ProductDetail = "405 2000" },
    ];

    public IEnumerable<string> GetOrderById(int id)
    {
        return orders.Where(i => i.Id == id).SelectMany(x => new[] { x.Address, x.ProductDetail });
    }

    public List<Order> GetAllOrders() { return orders; }
}
