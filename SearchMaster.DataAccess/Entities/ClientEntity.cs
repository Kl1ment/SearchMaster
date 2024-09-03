namespace SearchMaster.DataAccess.Entities
{
    public class ClientEntity : PersonEntity
    {
        public List<OrderEntity> Orders { get; set; } = [];
    }
}
