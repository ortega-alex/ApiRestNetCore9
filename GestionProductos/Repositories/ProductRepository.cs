using GestionProductos.DataAccess;
using GestionProductos.Interfaces;
using GestionProductos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProductos.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.ProductDetails)
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductDetails)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
                _context.Products.Remove(product);
        }       

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }      
    }
}
