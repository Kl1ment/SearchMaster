
using CSharpFunctionalExtensions;
using SearchMaster.Core.Enum;
using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Repositories
{
    public interface IPersonRepository
    {
        Task<IResult<string>> AddPerson(Person person);
        Task<IResult<Roles>> GetRole(Guid id);
        Task<IResult<Person>> GetByEmail(string email);
        Task<IResult<string>> Delete(Guid id);
    }
}