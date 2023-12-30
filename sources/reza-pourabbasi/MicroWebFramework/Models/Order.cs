namespace MicroWebFramework.Models;
public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string? Address { get; set; }
    public decimal Total { get; set; }

}