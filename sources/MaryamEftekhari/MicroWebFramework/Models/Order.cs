using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFramework.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Description { get; set; }
        public User Receiver { get; set; }
    }

    public class OrderService
    {        
        public IList<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            UserService userService = new UserService();
            var users = userService.GetUsers();
            var order1 = new Order
            {
                Id = 1,
                RegisterDate = DateTime.Now,
                Receiver = users.SingleOrDefault(u => u.Id == 1),
                Description = "اطلاعات اولین سفارش"
            };
            orders.Add(order1);

            var order2 = new Order
            {
                Id = 2,
                RegisterDate = DateTime.Now,
                Receiver = users.SingleOrDefault(u => u.Id == 2),
                Description = "اطلاعات دومین سفارش"
            };
            orders.Add(order2);

            var order3 = new Order
            {
                Id = 3,
                RegisterDate = DateTime.Now,
                Receiver = users.SingleOrDefault(u => u.Id == 3),
                Description = "اطلاعات سومین سفارش"
            };
            orders.Add(order3);

            return orders;
        }
    }
}
