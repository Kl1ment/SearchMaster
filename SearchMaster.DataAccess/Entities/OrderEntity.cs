
namespace SearchMaster.DataAccess.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public ClientEntity? Client { get; set; }
        public Guid ClientId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
