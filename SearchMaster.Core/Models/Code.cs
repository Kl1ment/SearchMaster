namespace SearchMaster.Core.Models
{
    public class Code(Guid id, string code)
    {
        public Guid Id { get; } = id;
        public string CodeHash { get; } = code;
    }
}
