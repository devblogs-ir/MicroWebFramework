namespace MicroWebFramework;

public class Order
{
    public int UserId { get; set; }
    public long Price { get; set; }
    public bool IsValid { get; set; }
    public List<String> Products{ get; set; }
}