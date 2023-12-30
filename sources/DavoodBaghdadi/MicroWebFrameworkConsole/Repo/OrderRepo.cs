using MicroWebFrameworkConsole.Models;

namespace MicroWebFrameworkConsole.Repo
{
    public class OrderRepo
    {
        List<Order> _orders;
        public OrderRepo()
        {
            _orders = new List<Order>()
            {
                new Order(1,13,123800),
                new Order(2,9,112000),
                new Order(3,3,67000),
                new Order(4,2,23500),
                new Order(5,1,38000),
            };
        }
        public List<Order> GetOrders()
        {
            return _orders;
        }

       
    }
}
