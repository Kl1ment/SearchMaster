namespace SearchMaster.DataAccess.Entities
{
    public class ReviewEntity
    {
        public Guid Id { get; set; }
        public PersonEntity? Writer { get; set; }
        public int Mark { get; set; }
        public string TextData { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public PersonEntity? Holder { get; set; }
        public Guid WriterId { get; set; }
        public Guid HolderId { get; set; }
    }
}
