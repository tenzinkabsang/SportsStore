using Microsoft.EntityFrameworkCore;

namespace SportsStore.Data
{
    public interface IProductRepository
    {
        (int totalNumberOfProducts, IList<Product> paginatedProducts) 
        GetProducts(string? category, int pageNumber, int pageSize);

        Task<List<Product>> GetAllProductsAsync();

        Task<List<Product>> GetProductsForCategory(string category, int count);

        Task<List<Product>> GetProductsWithIds(IList<int> productIds);

        Product GetProductById(int id);
        List<string> GetAllCategories();

        Task SaveProductAsync(Product product);
        Task CreateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _dbContext;
        public ProductRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Used named Tuple for now. If any more data needed - create a class!
        /// </summary>
        public (int totalNumberOfProducts, IList<Product> paginatedProducts) GetProducts(string? category, int pageNumber, int pageSize)
        {
            bool searchCriteria(Product p) => !p.IsDeleted && (category == null || p.Category == category);


            var products = _dbContext.Products
                .Include(p => p.Images)
                .Where(searchCriteria)
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalProductCount = _dbContext.Products
                .Where(searchCriteria)
                .Count();

            return (totalNumberOfProducts: totalProductCount, paginatedProducts: products);
        }

        public Product GetProductById(int id)
        {
            Product? p = _dbContext.Products
                .Include(p => p.Images)
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (p == null)
                throw new Exception($"Product with id={id} not found.");

            return p;
        }

        public List<string> GetAllCategories()
        {
            return _dbContext.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        
        public async Task<List<Product>> GetProductsWithIds(IList<int> productIds)
        {
            var products = await _dbContext.Products
                .Include(p => p.Images)
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetProductsForCategory(string category, int count)
        {
            var products = await _dbContext.Products
                .Where(p => p.Category == category)
                .Take(count)
                .ToListAsync();

            return products;
        }

        public async Task SaveProductAsync(Product product)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }
    }
}