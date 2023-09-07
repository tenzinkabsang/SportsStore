using Microsoft.AspNetCore.Mvc;
using SportsStore.Web.Models;

namespace SportsStore.Web.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {

        private readonly Cart _cart;

        public CartSummaryViewComponent(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
