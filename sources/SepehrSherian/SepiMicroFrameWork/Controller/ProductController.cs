using SepiMicroFrameWork.Configuration;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace SepiMicroFrameWork.Controller;

public class ProductController(HttpListenerContext Context)
{
    private IEnumerable<Product> Products => new[]
    {
                            new Product() {Name="Iphone15", Code="R-0001", Price=60000000, Id=1,Stock=100 },
                            new Product() {Name="Iphone14", Code="R-0002", Price=40000000, Id=2,Stock=100 },
                            new Product() {Name="Iphone13", Code="R-0003", Price=90000000, Id=3,Stock=100 },
                            new Product() {Name="Iphone14", Code="R-0004", Price=35000000, Id=4,Stock=100 },
    };

    public IEnumerable<Product> GetAllProducts()
    {
        var stringBuilder = new StringBuilder();
        var products = Products.ToList();
        foreach ( var product in products)
        {
            stringBuilder.AppendLine($"ID :{product.Id}");
            stringBuilder.AppendLine($"Name :{product.Name}");
            stringBuilder.AppendLine($"Code :{product.Code}");
            stringBuilder.AppendLine($"Price :{product.Price}");
            stringBuilder.AppendLine($"Stock :{product.Stock}");
            stringBuilder.AppendLine($"##################################");
        }
        var buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString());
        Context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        return products;
    }

    public Product GetProductByCode(string code)
    {
        var stringBuilder = new StringBuilder();
        var thisProduct = Products.FirstOrDefault(c => c.Code == code, new Product());
        stringBuilder.AppendLine($"ID :{thisProduct.Id}");
        stringBuilder.AppendLine($"Name :{thisProduct.Name}");
        stringBuilder.AppendLine($"Code :{thisProduct.Code}");
        stringBuilder.AppendLine($"Price :{thisProduct.Price}");
        stringBuilder.AppendLine($"Stock :{thisProduct.Stock}");
        var buffer = Encoding.UTF8.GetBytes(stringBuilder.ToString());
        Context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        return Products.FirstOrDefault(c => c.Code == code, new Product());
    }
}
