namespace SearchMaster.Core.Models
{
    public class Review
    {
        public const int MaxTextLength = 350;

        public Guid Id { get; }
        public Person? Writer { get; }
        public Guid WriterId { get; }
        public int Mark { get; }
        public string TextData { get; }
        public DateTime CreatedDate { get; }
        public Person? Holder { get; }
        public Guid HolderId { get; }

        private Review(
            Guid id,
            Person? writer,
            Guid writerId,
            int mark,
            string textData,
            DateTime createdDate,
            Person? holder,
            Guid holderId)
        {
            Id = id;
            Writer = writer;
            WriterId = writerId;
            Mark = mark;
            TextData = textData;
            CreatedDate = createdDate;
            Holder = holder;
            HolderId = holderId;
        }

        public static Review Create(
            Guid id,
            Person? writer,
            Guid writerId,
            int mark,
            string textData,
            DateTime createdDate,
            Person? holder,
            Guid holderId)
        {
            if (string.IsNullOrEmpty(textData))
                throw new ArgumentNullException("Text data cannot be empty");

            if (textData.Length > MaxTextLength)
                throw new ArgumentException($"Text data cannot be longer than {MaxTextLength} characters");

            return new Review(id, writer, writerId, mark, textData, createdDate, holder, holderId);
        }
    }
}
