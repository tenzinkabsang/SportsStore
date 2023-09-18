using SportsStore.Data;

namespace SportsStore.Web.Services
{
    public interface IRecommendationService
    {
        Task<IList<Product>> GetRecommendedItemsFor(Product product);
    }

    public class RecommendationService : IRecommendationService
    {
        private readonly IProductRepository _productRepo;
        public RecommendationService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<IList<Product>> GetRecommendedItemsFor(Product product)
        {
            var products = await _productRepo.GetProductsForCategory(product.Category, count: 5);

            var recommendedItems = products.Where(p => p.Id != product.Id).ToList();

            return recommendedItems;
        }
    }
}
