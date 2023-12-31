namespace Domain.Entities;

public  class Product : BaseEntity<int>
{ 
    public required string Title { get;  set; }
    public required decimal Price { get; set; }
    public string? ExpireDate { get; set; }
}