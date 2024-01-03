using System.ComponentModel.DataAnnotations;

namespace SepiMicroFrameWork;

public class Product
{
    public Product()
    {
        Code = null!;
        Name = null!;
        AddedDate= DateOnly.FromDateTime(DateTime.Now);
    }

    public int Id { get; set; }
    [MaxLength(8)]
    public string Code { get; set; }
    [MaxLength (100)]
    public string Name { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }
    public DateOnly AddedDate { get; set; }

}
