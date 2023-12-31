using Application.Dto;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace Controller;

public class ProductController 
{
    public async Task GetProductsAPIAsync() { 

        var services = new ServiceCollection();
        var container = new Container();
        container.Configure(config =>
        {   
            config.Scan(_ =>
                        {
                            _.AssemblyContainingType(typeof(Program));
                            _.WithDefaultConventions();
                        });
            config.Populate(services);
        });
     
        var serviceProvider = container.GetInstance<IServiceProvider>();
        var bar = serviceProvider.GetService<IServices>();
        var res = await bar.GetProduct();       
        Helper.Print(res.ToArray());

    } 
}
