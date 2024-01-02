
using MicroWebFramework.Model;
using MicroWebFramework.Services;
using System.Net;

namespace MicroWebFramework.Controller
{

    public class OrdersController
    {
        OrderService orderService = new OrderService();
        private readonly HttpListenerContext _HttpListenerContext;
        public OrdersController()
        {
            _HttpListenerContext = null!;
        }
        public OrdersController(HttpListenerContext HttpListenerContext)
        {
            _HttpListenerContext = HttpListenerContext;
        }
        public List<Order> Getall()
        {
            return orderService.GetAll();
        }

        public Order Getuserbyid(int Id)
        {
            return orderService.GetUserById(Id);
        }
    }
}
