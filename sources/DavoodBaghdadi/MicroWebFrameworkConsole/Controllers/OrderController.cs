using MicroWebFrameworkConsole;
using MicroWebFrameworkConsole.Repo;

namespace PipelineDesignPattern.Controllers
{

    public class OrderController
    {
        private readonly HTTPContext _httpContext;

        public OrderController(HTTPContext httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetOrders()
        {
            string orders = "Id \t" + "Quantity \t" + " Total Price \n";
            foreach (var o in new OrderRepo().GetOrders())
                orders += o.Id + "\t" +o.NumberOfProducts + "\t" + o.TotalPrice + "\n";
            return orders;
            return new OrderRepo().GetOrders().ToString();

        }

        public  string GetOrder(byte orderId)
        {
            var o = new OrderRepo().GetOrders().FirstOrDefault(o => o.Id == orderId);
            string products = "Id \t" + "Name \t" + " Description \n";
            return products += o.Id + "\t" + o.NumberOfProducts + "\t" + o.TotalPrice + "\n";
        }
    }
}
