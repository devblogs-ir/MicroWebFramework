using MicroWebFramework.Presentation.Data;
using MicroWebFramework.Presentation.Pipeline;

namespace MicroWebFramework.Presentation.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public ProductController(PipelineContext pipelineContext) : base(pipelineContext.HttpContext) { _context = new ApplicationContext(); }
        public void GetAllProducts()
        {
            Ok(_context.Products.ToList());
        }
        public void GetProductById(int id)
        {
            Ok(_context.Products.SingleOrDefault(i => i.Id == id));
        }
    }
}
