namespace SportsStore.Data
{
    public class Address : BaseEntity, ISoftDeletedEntity
    {
        public string Line1 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Zip { get; set; } = string.Empty;

        public bool Deleted { get; set; }
    }

}