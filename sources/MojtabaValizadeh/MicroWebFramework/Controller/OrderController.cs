using Dumpify;
using MicroWebFramework.Repository;

namespace MicroWebFramework;

public class OrderController
{
    private readonly Data _data;
    public OrderController()
    {
        _data = new Data();
    }
    public List<Order> GetAllorder()
    {
        return _data.Orders.ToList();
    }
    public List<Order> GetOrdersById(int id)
    {
        return _data.Orders.Where(a => a.UserId == id).ToList();
        
    }
}