using MicroWebFramework.Presentation.Data;
using MicroWebFramework.Presentation.Pipeline;

namespace MicroWebFramework.Presentation.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public UserController(PipelineContext pipelineContext) : base(pipelineContext.HttpContext) { _context = new ApplicationContext(); }
        public void GetAllUsers()
        {
            Ok(_context.Users.ToList());
        }
        public void GetUserById(int id)
        {
            Ok(_context.Users.SingleOrDefault(i => i.Id == id));
        }
    }
}
