using GestionProductos.DTOs.Products;
using GestionProductos.Models;

namespace GestionProductos.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(int id, Product product);
        Task DeleteAsync(int id);
    }
}
