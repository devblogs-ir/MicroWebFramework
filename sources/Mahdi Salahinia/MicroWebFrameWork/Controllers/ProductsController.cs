using MicroWebFrameWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebFrameWork.Controllers;

public class ProductsController
{
    private readonly List<Product> products =
    [
        new() {Id = 1, Name = "BMW"},
        new() {Id = 2, Name = "Benz"},
        new() {Id = 3, Name = "405"},
    ];

    public string GetProductById(int id)
    {
        var res = products.FirstOrDefault(i => i.Id == id);

        return res.Name;
    }

    public List<Product> GetAllProducts() { return products; }
}
