
namespace Domain.Entities;

public  class Order : BaseEntity<int>
{ 
   public required int UserId { get; set; }
   public required decimal TotalPrice { get; set; }
   public required List<Product> Products{ get; set; }

    
}