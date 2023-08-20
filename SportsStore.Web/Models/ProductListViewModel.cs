using SportsStore.Data;
using SportsStore.Models;

namespace SportsStore.Web.Models
{
    public class ProductListViewModel
    {
        public IList<Product> Products { get; set; } = new List<Product>();

        public PagingInfo PagingInfo { get; set; } = new();
    }
}
