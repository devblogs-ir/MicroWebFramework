using Domain.Entities;

namespace Domain.Contracts;

public interface IRepository 
{
  Task<IEnumerable<Product>> GetProductList();
  Task<IEnumerable<Order>> GetOrderList();
       
}