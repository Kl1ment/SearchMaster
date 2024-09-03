using CSharpFunctionalExtensions;
using SearchMaster.Core.Enum;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Repositories;

namespace SearchMaster.Application.Services
{
    public class StaffService(IPersonRepository personRepository) : IStaffService
    {
        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<IResult<string>> AddEmployee(string email, string name, string surname, string role)
        {
            if (Enum.TryParse(role, out Roles roleEnum))
            {
                var person = Person.Create(
                Guid.NewGuid(),
                email,
                name,
                surname,
                roleEnum);

                return await _personRepository.AddPerson(person);
            }

            return Result.Failure<string>("Invalid role");
        }

        public async Task<IResult<string>> DeletePerson(Guid id)
        {
            return await _personRepository.Delete(id);
        }
    }
}
