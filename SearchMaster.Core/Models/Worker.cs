using SearchMaster.Core.Enum;

namespace SearchMaster.Core.Models
{
    public class Worker : Person
    {
        public const int MaxAboutLength = 250;

        public string Username { get; }
        public string Profession { get; }
        public string About { get; }

        private Worker(
            Guid id,
            string email,
            string username,
            string name,
            string surname,
            string profession,
            string about,
            float rating,
            List<Review> reviews,
            Roles role)
            : base(id, email, name, surname, rating, reviews, role)
        {
            Username = username;
            Profession = profession;
            About = about;
        }

        public static Worker Create(
            Guid id,
            string email,
            string username,
            string name,
            string surname,
            string profession,
            string about,
            float rating,
            List<Review> reviews)
        {
            var validation = Validate(name, surname);

            if (validation.IsFailure)
                throw new ArgumentException(validation.Error);

            if (about.Length > MaxAboutLength)
                throw new ArgumentException($"About information cannot be longer than {MaxAboutLength} characters");

            return new Worker(id, email, username, name, surname, profession, about, rating, reviews, Roles.Worker);
        }

        public static Worker Create(
            Guid id,
            string email,
            string username,
            string name,
            string surname,
            string profession,
            string about)
        {
            var validation = Validate(name, surname);

            if (validation.IsFailure)
                throw new ArgumentException(validation.Error);

            if (about.Length > MaxAboutLength)
                throw new ArgumentException($"About information cannot be longer than {MaxAboutLength} characters");

            return new Worker(id, email, username, name, surname, profession, about, 0, [], Roles.Worker);
        }
    }
}
