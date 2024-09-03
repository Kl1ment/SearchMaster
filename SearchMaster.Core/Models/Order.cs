namespace SearchMaster.Core.Models
{
    public class Order
    {
        public const int MaxTitleLength = 50;
        public const int MaxDescriptionLength = 350;

        public Guid Id { get; }
        public Client? Client { get; }
        public Guid ClientId { get; }
        public string Title { get; }
        public string Description { get; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; }

        private Order(Guid id, Client? client, Guid clientId, string title, string description, decimal price, DateTime createdDate)
        {
            Id = id;
            Client = client;
            ClientId = clientId;
            Title = title;
            Description = description;
            Price = price;
            CreatedDate = createdDate;
        }

        public static Order Create(Guid id, Client? client, Guid clientId, string title, string description, decimal price, DateTime createdDate)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("Title cannot be empty");

            if (title.Length > MaxTitleLength)
                throw new ArgumentException($"Title cannot be longer than {MaxTitleLength} characters");

            if (description.Length > MaxDescriptionLength)
                throw new ArgumentException($"Description cannot be longer than {MaxDescriptionLength} characters");

            return new Order(id, client, clientId, title, description, price, createdDate);
        }
    }
}
