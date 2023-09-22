using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Data
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public string Category { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public int CategoryId { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();
    }


}