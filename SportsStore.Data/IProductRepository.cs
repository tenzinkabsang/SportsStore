namespace SportsStore.Data
{
    public interface IProductRepository
    {
        (int totalNumberOfProducts, IList<Product> paginatedProducts) GetProducts(int pageNumber, int pageSize);
        Product GetProductById(int id);
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
        public (int totalNumberOfProducts, IList<Product> paginatedProducts) GetProducts(int pageNumber, int pageSize)
        {
            var products = _dbContext.Products.Where(p => !p.IsDeleted)
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalProductCount = _dbContext.Products.Count();

            return (totalNumberOfProducts: totalProductCount, paginatedProducts: products);
        }

        public Product GetProductById(int id)
        {
            Product? p = _dbContext.Products.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (p == null)
                throw new Exception($"Product with id={id} not found.");

            return p;
        }
    }
}