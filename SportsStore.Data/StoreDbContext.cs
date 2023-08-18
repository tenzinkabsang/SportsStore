using Microsoft.EntityFrameworkCore;

namespace SportsStore.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) 
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
    }
}