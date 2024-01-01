using MicroWebFramework.Models;

namespace MicroWebFramework;

public class ProductController
{
    private readonly HttpContext _httpContext;
    private List<Product> _product;

    public ProductController(HttpContext httpContext)
    {
        _httpContext = httpContext;
        _product = new List<Product>
        {
            new Product { Id = 1, Name = "Product1" },
            new Product { Id = 2, Name = "Product2" },
            new Product { Id = 3, Name = "Product3" }
        };
    }

    public string GetAllProduct()
    {
        return $"Get all products";
    }

    public Product GetProductById(int productId) => _product.FirstOrDefault(p => p.Id == productId);
}