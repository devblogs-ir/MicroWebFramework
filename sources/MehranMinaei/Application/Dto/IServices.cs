using Domain.Entities;

namespace Application.Dto;

public interface IServices {
    Task<IEnumerable<Product>> GetProduct();
}