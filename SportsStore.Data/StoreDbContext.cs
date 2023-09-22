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
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Image> Images => Set<Image>();


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified))
                .ToList()
                .ForEach(entry =>
                {

                    ((BaseEntity)entry.Entity).ModifiedDate = DateTime.Now;

                    if (entry.State == EntityState.Added)
                        ((BaseEntity)entry.Entity).CreatedDate = DateTime.Now;

                });


            return base.SaveChangesAsync(cancellationToken);
        }

    }
}