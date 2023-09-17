namespace SportsStore.Data
{
    public class Image : BaseEntity
    {
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMainImage { get; set; }
        public Product Product { get; set; } = new();
    }


}