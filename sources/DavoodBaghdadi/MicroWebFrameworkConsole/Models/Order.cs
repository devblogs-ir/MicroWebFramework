namespace MicroWebFrameworkConsole.Models
{
    public class Order
    {
        public Order(byte id, byte no,double price)
        {
            Id = id;
            NumberOfProducts = no;
            TotalPrice=price;
        }
        public byte Id { get; set; }
        public byte NumberOfProducts { get; set; }

        public double TotalPrice { get; set; }
    }
}
