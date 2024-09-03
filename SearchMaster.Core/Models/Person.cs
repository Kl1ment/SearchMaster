using CSharpFunctionalExtensions;
using SearchMaster.Core.Enum;

namespace SearchMaster.Core.Models
{
    public class Person(Guid id, string email, string name, string surname, float rating, List<Review> reviews, Roles role)
    {
        public const int MaxLengthString = 30;

        public Guid Id { get; } = id;
        public string Email { get; } = email;
        public string Name { get; } = name;
        public string Surname { get; } = surname;
        public float Rating { get; } = rating;
        public List<Review> Reviews { get; } = reviews;
        public Roles Role { get; } = role;

        public static Person Create(Guid id, string email, string name, string surname, Roles role)
        {
            var validation = Validate(name, surname);

            if (validation.IsFailure)
                throw new ArgumentException(validation.Error);

            return new Person(id, email, name, surname, 0, [], role);
        }

        public static Result Validate(string name, string surname)
        {
            if (name.Length > MaxLengthString)
                return Result.Failure($"Name cannot be longer than {MaxLengthString} characters");

            if (surname.Length > MaxLengthString)
                return Result.Failure($"Surname cannot be longer than {MaxLengthString} characters");

            if (string.IsNullOrEmpty(name))
                return Result.Failure("Name cannot be empty");

            if (string.IsNullOrEmpty(surname))
                return Result.Failure("surname cannot be empty");

            return Result.Success();
        }
    }
}
