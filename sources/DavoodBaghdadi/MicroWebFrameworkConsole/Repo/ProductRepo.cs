using MicroWebFrameworkConsole.Models;

namespace MicroWebFrameworkConsole.Repo
{
    public class ProductRepo
    {
        private List<Product> _products { get; set; }
        public ProductRepo()
        {
            _products = new List<Product>()
            {
                 new Product(1,"Tee","Black Tee"),
                 new Product(2,"Coffee","Esperso"),
                 new Product(3,"Coke","Coca"),
                 new Product(4,"Fanta","Orange"),
                 new Product(5,"Sprite","Lemon")
            };
        }
        public IEnumerable<Product> GetProducts()
        {
          return _products;
        }
    }
}
