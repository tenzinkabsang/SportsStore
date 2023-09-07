using Microsoft.EntityFrameworkCore;

namespace SportsStore.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) 
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<CartItem> CartItems => Set<CartItem>();

    }
}