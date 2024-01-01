using MicroWebFramework.Models;

namespace PipelineDesignPattern.Controllers;
public class ProductsController : BaseController
{
    public ProductsController(HttpContext context) : base(context) { }

    IList<Products> Products = new List<Products>()
    {
        new() { Id = 1, Name = "Mobile", },
        new() { Id = 2, Name = "Monitor", },
        new() { Id = 3, Name ="Laptop", },
        new() { Id = 4, Name = "Speaker", },
    };

    public void GetAllProducts()
    {
        Ok(Products);
    }

    public void GetProductById(int id)
    {
        Ok(Products.FirstOrDefault(x => x.Id == id));
    }
}
