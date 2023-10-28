using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Data;
using SportsStore.Web.Controllers;
using SportsStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        //[Fact]
        //public void CannotCheckoutEmptyCart()
        //{
        //    var mock = new Mock<IOrderRepository>();
        //    var cart = new Cart();
        //    var order = new OrderViewModel();

        //    var target = new OrderController(mock.Object, cart);

        //    ViewResult? result = target.Checkout(order) as ViewResult;

        //    mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
        //    Assert.True(string.IsNullOrEmpty(result?.ViewName));
        //    Assert.False(result?.ViewData.ModelState.IsValid);
        //}

        //[Fact]
        //public void CannotCheckoutInvalidShippingDetails()
        //{
        //    var mock = new Mock<IOrderRepository>();
        //    var cart = new Cart();
        //    cart.AddItem(new Product(), 1);

        //    var order = new OrderViewModel();

        //    var target = new OrderController(mock.Object, cart);

        //    // Inject error
        //    target.ModelState.AddModelError("fatal", "fatal");

        //    ViewResult? result = target.Checkout(order) as ViewResult;


        //    mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
        //    Assert.True(string.IsNullOrEmpty(result?.ViewName));
        //    Assert.False(result?.ViewData.ModelState.IsValid);
        //}

        //[Fact]
        //public void CanCheckoutAndSubmitOrders()
        //{
        //    var mock = new Mock<IOrderRepository>();
        //    var cart = new Cart();
        //    cart.AddItem(new Product(), 1);

        //    var order = new OrderViewModel();

        //    var target = new OrderController(mock.Object, cart);

        //    RedirectToPageResult? result = target.Checkout(order) as RedirectToPageResult;
        //    mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
        //    Assert.Equal("/Completed", result?.PageName);

        //}
    }
}
