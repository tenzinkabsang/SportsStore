using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Data;
using SportsStore.Web.Components;

namespace SportsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void ShouldUseCategoriesReturnedFromRepository()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetAllCategories()).Returns(new List<string>
            {
                "Shorts",
                "Pants",
                "Dresses"
            });

            var target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext
                {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            };
            target.RouteData.Values["category"] = "Pants";

            List<string> categories = ((IEnumerable<string>?)((ViewViewComponentResult)target.Invoke())?.ViewData?.Model ?? Enumerable.Empty<string>()).ToList();

            Assert.Equal("Shorts", categories[0]);
            mock.Verify(m => m.GetAllCategories(), Times.Once());
        }

    }
}
