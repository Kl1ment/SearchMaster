using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SearchMaster.Core.Enum;
using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Repositories
{
    public class PersonRepository(SearchMasterDbContext context) : IPersonRepository
    {
        private readonly SearchMasterDbContext _context = context;

        public async Task<IResult<string>> AddPerson(Person person)
        {
            try
            {
                var personEntity = person.MapToEntity();

                await _context.Persons.AddAsync(personEntity);
                await _context.SaveChangesAsync();

                return Result.Success($"Client {personEntity.Id} has been added");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.ToString());
            }
        }

        public async Task<IResult<Roles>> GetRole(Guid id)
        {
            var role = await _context.Persons
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => p.RoleEntity)
                .FirstOrDefaultAsync();

            if (Enum.TryParse(role?.Name, out Roles roleEnum))
                return Result.Success(roleEnum);

            return Result.Failure<Roles>("The role was not found");
        }

        public async Task<IResult<Person>> GetByEmail(string email)
        {
            var personEntity = await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Email == email);

            if (personEntity == null)
                return Result.Failure<Person>("The account was not found");

            var person = personEntity.MapToModel();

            return Result.Success(person);
        }

        public async Task<IResult<string>> Delete(Guid id)
        {
            try
            {
                await _context.Persons
                    .Where(p => p.Id == id)
                    .ExecuteDeleteAsync();

                await _context.SaveChangesAsync();

                return Result.Success("Person has been deleted");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                return Result.Failure<string>(ex.Message);
            }
        }
    }
}
