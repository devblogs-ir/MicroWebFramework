using MicroWebFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Services
{
    internal class OrderService
    {
        public List<Order> GetAll()
        {
            var orders = new List<Order>();
            orders.Add(new Order()
            {
                orderName = "Order 1",
                Count = 3
            });

            orders.Add(new Order()
            {
                orderName = "Order 2",
                Count = 10
            });
            orders.Add(new Order()
            {
                orderName = "Order 3",
                Count = 6
            });


            return orders;
        }

        public Order GetUserById(int id)
        {
            var orders = GetAll();
            return orders[id];
        }
    }
}
