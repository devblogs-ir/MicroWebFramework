using MicroWebFramework.Models;
using MicroWebFramework.Mvc;

namespace MicroWebFramework.Controllers;
public class OrdersController : ControllerBase
{
    private readonly IEnumerable<Order> orders;
    public OrdersController()
    {
        orders = new List<Order>()
        {
            new Order(){Id= 1,CustomerId = 1,Address = "address 1",Total = 105},
            new Order(){Id= 2,CustomerId = 1,Address = "address 2",Total = 80},
            new Order(){Id= 3,CustomerId = 2,Address = "address 3",Total = 200},
        };
    }
    public void GetAll()
    {
        Ok(orders);
    }
    public void GetById(int id)
    {
        var result = orders.FirstOrDefault(p => p.Id == id);
        if (result is null)
            NotFound("not found");

        Ok(result);
    }
}