namespace SearchMaster.DataAccess.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<PersonEntity>? Persons { get; set; }
    }
}
