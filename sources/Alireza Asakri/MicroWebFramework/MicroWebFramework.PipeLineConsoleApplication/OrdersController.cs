using Dumpify;


namespace MicroWebFramework.PipeLineConsoleApplication
{
    public class OrdersController(HttpContext httpContext)
    {
        public void GetAll()
        {
            $"User Id {httpContext.UserId}".Dump("Return all orders");
        }
        public void GetByUserId(int id)
        {
            $"Return by user id: {id}".Dump();
        }
    }
}
