using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models;
using SportsStore.Web.Models;
using SportsStore.Web.Services;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IRecommendationService _recommendationService;
        private readonly int PageSize;

        public HomeController(IProductRepository productRepo, IConfiguration config,
            IRecommendationService recommendationService)
        {
            _productRepository = productRepo;
            _recommendationService = recommendationService;

            PageSize = config.GetValue<int>("WebApp:PageSize");
        }

        public async Task<ViewResult> Detail(int productId, string returnUrl)
        {
            var product = _productRepository.GetProductById(productId);

            var recommendedItems = await _recommendationService.GetRecommendedItemsFor(product);

            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                ReturnUrl = returnUrl,
                RecommendedItems = recommendedItems
            };

            return View(viewModel);
        }

        public ViewResult Index(string? category, int productPage = 1)
        {

            (int totalNumberOfProducts, IList<Product> paginatedProducts) products = 
                _productRepository.GetProducts(category, productPage, PageSize);

            var viewModel = new ProductListViewModel
            {
                Products = products.paginatedProducts,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = products.totalNumberOfProducts
                },
                CurrentCategory = category
            };

            return View(viewModel);
        }

    }
}