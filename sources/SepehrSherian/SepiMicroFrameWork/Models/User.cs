using System.ComponentModel.DataAnnotations;

namespace SepiMicroFrameWork;

public class User
{
    public User()
    {
        Id = Guid.NewGuid();
        FirstName=null!; 
        LastName=null!;
        PhoneNumber=null!;
    }

    public Guid Id { get; set; }
    [MaxLength(30)]
    public string FirstName { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; }
    [MaxLength(11)]
    public string PhoneNumber { get; set; }
}
