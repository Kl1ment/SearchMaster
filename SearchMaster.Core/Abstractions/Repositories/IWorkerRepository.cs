using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Repositories
{
    public interface IWorkerRepository
    {
        Task<IResult<string>> Add(Worker worker);
        Task<IResult<string>> Delete(Guid id);
        Task<IResult<Worker>> GetByUsername(string username);
        Task<IResult<Worker>> GetByEmail(string email);
        Task<IResult<Worker>> GetById(Guid id);
        Task<IResult<string>> Update(Worker worker);
        Task<int?> GetMaxUsernameNumber(string username);
    }
}