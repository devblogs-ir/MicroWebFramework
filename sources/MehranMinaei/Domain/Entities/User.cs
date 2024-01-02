namespace Domain.Entities;

public  class User : BaseEntity<int>
{ 
  public required string UserName { get; set; }
}