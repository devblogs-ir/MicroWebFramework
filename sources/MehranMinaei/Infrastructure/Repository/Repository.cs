using Domain.Contracts;
using Domain.Entities;

namespace Infrastructure.Repository;

public record AdvertiseCreateResponse(string Id);
public class Repository : IRepository
{

    public async Task<IEnumerable<Product>> GetProductList()
    {
        
        var Products = new List<Product>() { new Product { Id = 1, Price = 150, Title = "Product Name 1" } } ;
        Console.WriteLine(Products);

        return Products;
    }

    public async Task<IEnumerable<Order>> GetOrderList()
    {
         var Orders = new List<Order>() { new Order { Products = new List<Product>() { new Product { Id=1, Price=150,Title="Product Name 1"}  } , TotalPrice = 10, UserId = 1 } }; 
        return Orders;
    }


   
}

