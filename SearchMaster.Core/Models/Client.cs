using SearchMaster.Core.Enum;

namespace SearchMaster.Core.Models
{
    public class Client : Person
    {
        public List<Order> Orders { get; }

        private Client(Guid id, string email, string name, string surname, float rating, List<Review> reviews, List<Order> orders, Roles role)
            : base(id, email, name, surname, rating, reviews, role)
        {
            Orders = orders;
        }

        public static Client Create(Guid id,
            string email,
            string name,
            string surname,
            float rating,
            List<Review> reviews,
            List<Order> orders)
        {
            var validation = Validate(name, surname);

            if (validation.IsFailure)
                throw new ArgumentException(validation.Error);

            return new Client(id, email, name, surname, rating, reviews, orders, Roles.Client);
        }

        public static Client Create(
            Guid id,
            string email,
            string name,
            string surname)
        {
            var validation = Validate(name, surname);

            if (validation.IsFailure)
                throw new ArgumentException(validation.Error);

            return new Client(id, email, name, surname, 0, [], [], Roles.Client);
        }
    }
}
