namespace MicroWebFrameWork;

public interface ProductListFiltersType
{
    public string? Search { get; set; }
    public byte PageSize { get; set; }
    public uint PageNumber { get; set; }
}

public class ProductsController : ProductListFiltersType
{
    public string? Search { get; set; }

    public byte PageSize { get; set; }

    public uint PageNumber { get; set; }
    public ProductsController() { }
    public ProductsController(ProductListFiltersType info)
    {
        Search = info.Search;
        PageSize = info.PageSize;
        PageNumber=info.PageNumber;
    }

    private readonly HttpContext _Content;

    public ProductsController(HttpContext content)
    {
        _Content = content;
    }

    public string GetAll(HttpContext httpContext)
    {
        Console.WriteLine("Get all Products");
        return "All Products";
    }
}
