using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SportsStore.Data;
using SportsStore.Web.Models;

namespace SportsStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepo;
        private readonly Cart cart;
        private readonly ILogger _logger;
        public OrderController(IOrderRepository orderRepo, Cart cart, ILoggerFactory loggerFactory)
        {
            this.orderRepo = orderRepo;
            this.cart = cart;
            _logger = loggerFactory.CreateLogger("Tenzin");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.Cart = cart;
            base.OnActionExecuting(context);
        }

        public IActionResult Checkout()
        {
            return View(new OrderViewModel());
        }

        [HttpPost]
        public IActionResult Checkout(OrderViewModel model)
        {
            if (!cart.CartItems.Any())
                ModelState.AddModelError("", "Sorry, your cart is empty!");

            if (ModelState.IsValid)
            {
                model.Items = cart.CartItems;

                // Use a mapper or add a service to handle all this!
                var order = new Order
                {
                    Name = model.FullName,
                    GiftWrap = model.GiftWrap,
                    Address = new Address
                    {
                        Line1 = model.Line1,
                        City = model.City,
                        State = model.State,
                        Zip = model.Zip
                    },
                    OrderItems = cart.CartItems
                                    .Select(c => new OrderItem { Quantity = c.Quantity, Product = c.Product }).ToList()
                };


                orderRepo.SaveOrder(order);
                cart.Clear();


                _logger.LogDebug("Checkout Success. {Order}", order.ToJsonMs());

                return RedirectToPage("/Completed", new { OrderId = order.Id });
            }
            else
            {
                return View();
            }
        }
    }
}
