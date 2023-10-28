using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models;
using SportsStore.Web;
using SportsStore.Web.Models;
using SportsStore.Web.Services;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IRecommendationService _recommendationService;
        private readonly int PageSize;
        private readonly ILogger _logger;

        public HomeController(IProductRepository productRepo, IConfiguration config,
            IRecommendationService recommendationService, ILoggerFactory loggerFactory)
        {
            _productRepository = productRepo;
            _recommendationService = recommendationService;

            _logger = loggerFactory.CreateLogger("Tenzin");

            PageSize = config.GetValue<int>("WebApp:PageSize");
        }

        public async Task<ViewResult> Detail(int productId, string returnUrl)
        {


            using (_logger.BeginScope("Start: Detail action. {ProductId} and {ReturnUrl}", productId, returnUrl))
            {
                _logger.LogInformation("Inside Scope");
                var product = _productRepository.GetProductById(productId);

                var recommendedItems = await _recommendationService.GetRecommendedItemsFor(product);

                var viewModel = new ProductDetailViewModel
                {
                    Product = product,
                    ReturnUrl = returnUrl,
                    RecommendedItems = recommendedItems
                };

                _logger.LogInformation("End: Detail action. {ViewModel}", viewModel.ToJsonMs());
                return View(viewModel);
            }

        }

        public ViewResult Index(string? category, int productPage = 1)
        {



            (int totalNumberOfProducts, IList<Product> paginatedProducts) products = 
                _productRepository.GetProducts(category, productPage, PageSize);


            _logger.LogInformation("Index action. {PaginatedProducts}", products.paginatedProducts.ToJsonMs());



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