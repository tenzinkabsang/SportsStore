namespace SportsStore.Data
{
    public class OrderItem : BaseEntity
    {
        public int Quantity { get; set; }
        public Product Product { get; set; } = new();
    }

}