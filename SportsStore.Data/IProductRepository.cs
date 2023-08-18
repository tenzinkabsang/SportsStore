namespace SportsStore.Data
{
    public interface IProductRepository
    {
        IList<Product> GetProducts();
        Product GetProductById(int id);
    }

    public class ProductRepository: IProductRepository
    {
        private readonly StoreDbContext _dbContext;
        public ProductRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Product> GetProducts()
        {
            var products = _dbContext.Products.ToList();

            return products;
        }

        public Product GetProductById(int id)
        {
            Product? p = _dbContext.Products.FirstOrDefault(x => x.Id == id);

            if (p == null)
                throw new Exception($"Product with id={id} not found.");

            return p;
        }
    }
}