namespace MicroApiFramework
{
    public class OrderController
    {
        private readonly HttpContext _httpContext;

        public OrderController(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public void GetAll()
        {
           _httpContext.Response +="return all";
        }
    }
}
