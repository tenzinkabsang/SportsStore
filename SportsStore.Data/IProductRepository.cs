using Microsoft.EntityFrameworkCore;

namespace SportsStore.Data
{
    public interface IProductRepository
    {
        (int totalNumberOfProducts, IList<Product> paginatedProducts) 
        GetProducts(string? category, int pageNumber, int pageSize);

        Task<IList<Product>> GetProductsForCategory(string category, int count);

        Task<IList<Product>> GetProductsWithIds(IList<int> productIds);

        Product GetProductById(int id);
        List<string> GetAllCategories();
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

        
        public async Task<IList<Product>> GetProductsWithIds(IList<int> productIds)
        {
            var products = await _dbContext.Products
                .Include(p => p.Images)
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            return products;
        }

        public async Task<IList<Product>> GetProductsForCategory(string category, int count)
        {
            var products = await _dbContext.Products
                .Where(p => p.Category == category)
                .Take(count)
                .ToListAsync();

            return products;
        }
    }
}