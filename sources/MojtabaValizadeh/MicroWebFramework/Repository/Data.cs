namespace MicroWebFramework.Repository;

public class Data
{
    public IEnumerable<Order> Orders { get; }
    public IEnumerable<User> Users { get; }
    public Data()
    {
         Orders = new List<Order>
        {
            new() { UserId = 1, IsValid = true, Price = 200000,Products = new()
            {
                "Mobile",
                "Headphone",
                "Tablet"
            }},
            new() { UserId = 2, IsValid = true, Price = 30000,Products = new()
            {
                "Headphone",
                "Tablet"
            }}
        };
         Users = new List<User>
        {
            new() { Name = "Mojtaba Valizadeh", Id = 1, IsExpired = true },
            new()  { Name = "Nabi Karampour", Id = 2, IsExpired = false },
            new () { Name = "Mohammad Karimi ", Id = 3, IsExpired = false },
            new () { Name = "Masoud Hajizadeh ", Id = 4, IsExpired = false }

        };
    }
}