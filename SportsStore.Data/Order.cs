namespace SportsStore.Data
{
    public class Order : BaseEntity
    {

        public string Name { get; set; } = string.Empty;

        public bool GiftWrap { get; set; }

        public Address Address { get; set; } = new();

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public bool IsShipped { get; set; }

    }

}