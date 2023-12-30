namespace MicroWebFrameworkConsole.Models
{

    public class Product
    {
        List<Product> products=new List<Product>();
       
        public Product(int id, string name, string des)
        {
            Id = id; Name = name; Description = des;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    };
}

