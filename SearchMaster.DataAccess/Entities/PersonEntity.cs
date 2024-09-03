using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Entities
{
    public class PersonEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public float Rating { get; set; }
        public List<ReviewEntity> Reviews { get; set; } = [];
        public RoleEntity RoleEntity { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
