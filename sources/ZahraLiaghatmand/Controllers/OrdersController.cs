namespace PipelineDesignPattern.Controllers;
public class OrdersController
{
    private readonly HttpContext _context;
    public OrdersController(HttpContext context)
    {
        _context = context;
    }
    public void GetAll()
    {
        Console.WriteLine("all orders");
        _context.Response += "all orders";
    }
}