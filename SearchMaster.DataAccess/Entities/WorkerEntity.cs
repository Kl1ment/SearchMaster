namespace SearchMaster.DataAccess.Entities
{
    public class WorkerEntity : PersonEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Profession { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
    }
}
