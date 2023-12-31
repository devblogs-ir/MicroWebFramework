using Dumpify;
using MicroWebFramework.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MicroWebFramework.Controller;

public class OrdersController : Controller
{
    public readonly IEnumerable<Order> data;
    public OrdersController(HttpContext httpContext) : base(httpContext)
    {
        data = new List<Order>()
        {
            new Order(){Id= 1,Name= "Order1"},
            new Order(){Id= 2,Name= "Order2"},
            new Order(){Id= 3,Name= "Order3"},
            new Order(){Id= 3,Name= "Order4"}
        };
    }



    public void GetAll()
    {
        Ok(data.ToList());
    }
    public void GetByID(int Id)
    {
        var result_ = data.Where(x => x.Id == Id).FirstOrDefault();
        Ok(result_);


    }

}
