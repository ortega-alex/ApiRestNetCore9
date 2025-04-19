using GestionProductos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionProductos.DataAccess
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optiones): base(optiones)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameof(BaseEntity.CreatedAt)).CurrentValue = DateTime.Now;
                    entry.Property(nameof(BaseEntity.UpdatedAt)).CurrentValue = DateTime.Now;
                } else if (entry.State == EntityState.Modified)
                    entry.Property(nameof(BaseEntity.UpdatedAt)).CurrentValue = DateTime.Now;
            }
            return base.SaveChanges();
        }
    }
}
