using MicroWebFramework.Common;
using MicroWebFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroWebFramework.Controllers;
public class SetOrderController(HttpContext httpContext)
{
    private readonly HttpContext _httpContext = httpContext;

    List<Order> orders = new()
    {
        new Order() { Id = 1, Name = "Order1" },
        new Order() { Id = 2, Name = "Order2" },
    };

    public void GetAllOrders()
    {
        string ordersList = JsonSerializer.Serialize(orders, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        _httpContext.Response.OutputStream.Write(Encoding.UTF8.GetBytes(ordersList));
    }


    public void GetOrderById(int id)
    {
        if (!orders.Any(p => p.Id == id))
        {
            _httpContext.Response.OutputStream.Write(Encoding.UTF8.GetBytes($"No order was found with id: {id}!"));
            return;
        }
        _httpContext.Response.OutputStream.Write(Encoding.UTF8.GetBytes(orders.SingleOrDefault(p => p.Id == id).Name));
        return;
    }
}

