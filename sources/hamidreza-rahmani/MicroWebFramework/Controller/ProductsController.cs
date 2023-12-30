using MicroWebFramework.Model;

namespace MicroWebFramework.Controller;

public class ProductsController
{
    private readonly IEnumerable<Product> _products;

    public ProductsController()
    {
        _products = new List<Product>
        {
            new() { Id = 1, Title = "pizza", Price = 10 },
            new() { Id = 2, Title = "fish and chips", Price = 4 },
            new() { Id = 1, Title = "kebab", Price = 8 }
        };
    }

    public List<Product> GetAll()
    {
        return _products.ToList();
    }

    public Product GetById(long id)
    {
        return _products.ToList().Where(a => a.Id == id).FirstOrDefault();
    }
}