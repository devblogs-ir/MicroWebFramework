using Application.Dto;
using Domain.Entities;
using Infrastructure.Repository;
using Microsoft.VisualBasic;

namespace Application.Services;

public class Services:IServices { 
    private Repository repository { get; set; }
    
    public Services(Repository repository_) {
        repository = repository_;
    } 

    public async Task<IEnumerable<Product>> GetProduct()
    {
       
        var products = await repository.GetProductList();


        return products;
    }
}