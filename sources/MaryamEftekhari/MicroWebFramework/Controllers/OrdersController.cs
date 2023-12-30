using MicroWebFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Controllers
{
    public class OrdersController
    {
        public IList<Order> GetAll()
        {
            OrderService orderService = new OrderService();
            return orderService.GetOrders();
        }

        public Order GetByUserId(int userId)
        {
            OrderService orderService = new OrderService();
            return orderService
                .GetOrders()
                .SingleOrDefault(o => o.Receiver.Id == userId);
        }
    }
}
