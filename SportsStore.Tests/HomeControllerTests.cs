using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SportsStore.Controllers;
using SportsStore.Data;
using SportsStore.Models;
using SportsStore.Web.Models;

namespace SportsStore.Tests
{
    public class HomeControllerTests
    {
        private IConfiguration InMemoryConfig;

        public HomeControllerTests()
        {
            var inMemoryAppSetting = new Dictionary<string, string> { ["WebApp:PageSize"] = "2" };

            InMemoryConfig = new ConfigurationBuilder().AddInMemoryCollection(inMemoryAppSetting).Build();
        }

        [Fact]
        public void Can_Use_Repository()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts(It.IsAny<string?>(), It.IsAny<int>(), It.IsAny<int>())).Returns((2, new List<Product>
            {
                new Product { Id = 1, Name = "P1"},
                new Product { Id = 2, Name = "P2"}
            }));


            HomeController controller = new HomeController(mock.Object, InMemoryConfig);

            ProductListViewModel viewModel = controller.Index(null)?.ViewData.Model as ProductListViewModel ?? new();

            //Assert
            Assert.True(viewModel.Products.Count == 2);
            Assert.Equal("P1", viewModel.Products[0].Name);
        }

        [Fact]
        public void CanGetProductById()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProductById(It.IsAny<int>())).Returns(
                new Product { Id = 2, Name = "PXX" }
            );
            HomeController controller = new HomeController(mock.Object, InMemoryConfig);

            Product product = controller.Detail(2).ViewData?.Model as Product ?? new();

            // Assert
            Assert.Equal("PXX", product.Name);
        }

        [Fact]
        public void Can_Send_Pagination_ViewModel()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.GetProducts(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns((5, new List<Product>
            {
                new Product { Id = 1, Name = "P1"},
                new Product { Id = 2, Name = "P2"},
                new Product { Id = 3, Name = "P3"},
                new Product { Id = 4, Name = "P4"},
                new Product { Id = 5, Name = "P5"},
            }));

            var controller = new HomeController(mock.Object, InMemoryConfig);

            // Act
            ProductListViewModel viewModel = controller.Index(null, 2)?.ViewData.Model as ProductListViewModel ?? new();

            // Assert
            PagingInfo pageInfo = viewModel.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(2, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(3, pageInfo.TotalPages);
        }

    }
}