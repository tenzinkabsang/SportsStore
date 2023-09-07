using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;

namespace SportsStore.Web.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {

        private readonly IProductRepository _productRepository;

        public NavigationMenuViewComponent(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke()
        {

            List<string> categories = _productRepository.GetAllCategories();

            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(categories);
        }
    }
}
