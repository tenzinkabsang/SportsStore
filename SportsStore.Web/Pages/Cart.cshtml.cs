using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Data;
using SportsStore.Web.Infrastructure;
using SportsStore.Web.Models;

namespace SportsStore.Web.Pages
{
    public class CartModel : PageModel
    {
        private IProductRepository _productRepository;

        private const string BASE_URL = "/";

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = BASE_URL;


        public CartModel(IProductRepository productRepository, Cart cart)
        {
            _productRepository = productRepository;
            Cart = cart;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? BASE_URL;
        }

        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product product = _productRepository.GetProductById(productId);

            Cart.AddItem(product, 1);

            return RedirectToPage(new { returnUrl });
        }

        public IActionResult OnPostRemove(int productId, string returnUrl)
        {
            Product product = Cart.CartItems.Where(c => c.Product.Id == productId).Select(c => c.Product).First();
            Cart.RemoveLine(product);
            return RedirectToPage(new { returnUrl });
        }

    }
}
