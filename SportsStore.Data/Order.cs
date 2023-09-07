namespace SportsStore.Data
{
    public class Order : BaseEntity
    {

        public string Name { get; set; } = string.Empty;

        public bool GiftWrap { get; set; }

        public Address Address { get; set; } = new();

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }


    public class CartItem : BaseEntity
    {
        public int Quantity { get; set; }
        public Product Product { get; set; } = new();
    }

    public class Address : BaseEntity
    {
        public string Line1 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Zip { get; set; } = string.Empty;
    }

}