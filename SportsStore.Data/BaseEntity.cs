namespace SportsStore.Data
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }

    public interface ISoftDeletedEntity
    {
        bool Deleted { get; set; }
    }

}