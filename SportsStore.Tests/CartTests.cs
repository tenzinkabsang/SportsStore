using SportsStore.Data;
using SportsStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void CanAddNewItems()
        {
            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };

            var cart = new Cart();

            cart.AddItem(product: p1, quantity: 1);
            cart.AddItem(product: p2, quantity: 1);
            List<LineItem> items = cart.CartItems;

            Assert.Equal(2, items.Count);
            Assert.Equal("P1", items[0].Product.Name);
            Assert.Equal("P2", items[1].Product.Name);
        }

        [Fact]
        public void ShouldIncrementQuantityIfExistingItemsInCart()
        {
            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };

            var cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 5);

            List<LineItem> items = cart.CartItems.OrderBy(c => c.Product.Id).ToList();

            Assert.Equal(2, items.Count);
            Assert.Equal(6, items[0].Quantity);
            Assert.Equal(1, items[1].Quantity);

        }

        [Fact]
        public void CanRemoveItemFromCart()
        {
            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };

            var cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);

            cart.RemoveLine(p2);

            Assert.Empty(cart.CartItems.Where(c => c.Product.Id == 2));
            Assert.Single(cart.CartItems);
        }

        [Fact]
        public void CalculateCartTotal()
        {
            var p1 = new Product { Id = 1, Name = "P1", Price = 100M };
            var p2 = new Product { Id = 2, Name = "P2", Price = 50M };

            var cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 2);

            decimal total = cart.CartTotal;

            Assert.Equal(350M, total);
        }

        [Fact]
        public void CanClearAllItemsFromCart()
        {
            Product p1 = new Product { Id = 1, Name = "P1" };
            Product p2 = new Product { Id = 2, Name = "P2" };

            var cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);

            cart.Clear();

            Assert.Empty(cart.CartItems);
        }
    }
}
