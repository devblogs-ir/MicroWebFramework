namespace MicroApiFramework
{
    public class ProductController
    {
        private readonly HttpContext _httpContext;

        public ProductController(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }
        public void GetAll()
        {
            _httpContext.Response += "all product";
        }

        public void GetProductById(int Id)
        {
            var items = new List<string>
            {
                "product1",
                "product2",
                "product3"
            };

            _httpContext.Response += $"Product{Id}:{items[Id]}";
        }
    }
}
