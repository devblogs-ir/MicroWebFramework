using MicroWebFramework.Models;
using MicroWebFramework.Mvc;

namespace MicroWebFramework.Controllers;
public class ProductsController : ControllerBase
{
    private readonly IEnumerable<Product> products;
    public ProductsController()
    {
        products = new List<Product>()
        {
            new Product(){Id= 1,Title= "pizza",Price=10},
            new Product(){Id= 2,Title= "fish and chips",Price=4},
            new Product(){Id= 1,Title= "kebab",Price=8}
        };
    }
    public void GetAll()
    {
        Ok(products);
    }
    public void GetById(int id)
    {
        var result = products.FirstOrDefault(p => p.Id == id);
        if (result is null)
            NotFound("not found");

        Ok(result);
    }
}