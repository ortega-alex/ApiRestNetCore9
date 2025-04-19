using AutoMapper;
using GestionProductos.DTOs.Products;
using GestionProductos.Interfaces;
using GestionProductos.Models;

namespace GestionProductos.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var pruducts = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(pruducts);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task AddAsync(Product product)
        {
            await _repository.AddAsync(product);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(int id, Product product)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return;

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.UpdatedAt = DateTime.Now;
            existing.ProductDetails = product.ProductDetails;

            await _repository.UpdateAsync(existing);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }
    }
}
