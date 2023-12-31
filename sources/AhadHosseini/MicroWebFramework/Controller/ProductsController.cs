using Dumpify;
using MicroWebFramework.Models;

namespace MicroWebFramework.Controller;

public class ProductsController : Controller
{
    public readonly IEnumerable<Product> data;

    public ProductsController(HttpContext httpContext) : base(httpContext)
    {
        data = new List<Product>()
        {
            new Product(){Id= 1,Name= "Mobile"},
            new Product(){Id= 2,Name= "RAM"},
            new Product(){Id= 3,Name= "CPU"}
        };
    }

    public void GetAll()
    {
        var result_ = data.ToList();
        Ok(result_);
    }
    public void GetByID(int Id)
    {
        var result_ = data.Where(x => x.Id == Id).FirstOrDefault();
        Ok(result_);
    }
}
