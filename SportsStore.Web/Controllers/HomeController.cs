using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models;
using SportsStore.Web.Models;
using System.Diagnostics;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private const string PAGESIZE = "WebApp:PageSize";
        private readonly IProductRepository _productRepository;
        private readonly IConfiguration _config;

        public HomeController(IProductRepository productRepo, IConfiguration config)
        {
            _productRepository = productRepo;
            _config = config;
        }

        public ViewResult Detail(int productId)
        {
            var product = _productRepository.GetProductById(productId);

            return View(product);
        }

        public ViewResult Index(int page = 1)
        {
            int pageSize = _config.GetValue<int>(PAGESIZE);

            (int totalNumberOfProducts, IList<Product> paginatedProducts) products = _productRepository.GetProducts(page, pageSize);

            var viewModel = new ProductListViewModel
            {
                Products = products.paginatedProducts,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = products.totalNumberOfProducts
                }
            };

            return View(viewModel);
        }

    }
}