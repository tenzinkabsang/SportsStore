using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Web.Models
{
    public class OrderViewModel
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<LineItem> Items { get; set; } = new List<LineItem>();

        [Required(ErrorMessage ="Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the first address")]
        public string Line1 { get; set; }

        [Required(ErrorMessage ="Please enter a city name")]
        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        [DisplayName("Gift wrap these items")]
        public bool GiftWrap { get; set; }

        [BindNever]
        public bool IsShipped { get; set; }

        public decimal OrderTotal() => Items.Sum(i => i.Product.Price * i.Quantity);
    }
}
