using MicroWebFramework.Model;

namespace MicroWebFramework.Controller;

public class OrderController
{
    private readonly IEnumerable<Order> _orders;

    public OrderController()
    {
        _orders = new List<Order>
        {
            new() { Id = 1, Address = "Tehran", CustomerId = 10, Total = 10 },
            new() { Id = 2, Address = "tabriz", CustomerId = 11, Total = 10 },
            new() { Id = 3, Address = "zanjan", CustomerId = 12, Total = 10 },
            new() { Id = 4, Address = "takestan", CustomerId = 13, Total = 10 }
        };
    }

    public List<Order> GetAll()
    {
        return _orders.ToList();
    }

    public Order GetById(long id)
    {
        return _orders.ToList().Where(a => a.Id == id).FirstOrDefault();
    }
}