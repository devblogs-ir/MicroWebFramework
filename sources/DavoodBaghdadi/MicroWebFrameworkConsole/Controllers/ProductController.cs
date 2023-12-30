using MicroWebFrameworkConsole;
using MicroWebFrameworkConsole.Repo;

namespace PipelineDesignPattern.Controllers
{
    public class ProductController
    {
        private readonly HTTPContext _httpContext;

        public ProductController(HTTPContext httpContext)
        {
            _httpContext = httpContext;
        }
        public string GetProducts()
        {
            string products="Id \t"+"Name \t"+" Description \n";
            foreach(var p in new ProductRepo().GetProducts())
                products +=p.Id+ "\t" + p.Name+ "\t" + p.Description+"\n";
            return products;
        }
        public string GetProduct(byte productId)
        {
            var p = new ProductRepo().GetProducts().FirstOrDefault(p => p.Id == productId);
            string products = "Id \t" + "Name \t" + " Description \n";
            return products += p.Id + "\t" + p.Name + "\t" + p.Description + "\n";
        }
    }
}
