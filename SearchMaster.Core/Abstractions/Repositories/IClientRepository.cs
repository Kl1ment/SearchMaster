using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Repositories
{
    public interface IClientRepository
    {
        Task<IResult<Guid>> Add(Client client);
        Task<IResult<string>> Delete(Guid id);
        Task<List<Client>> Get();
        Task<IResult<Client>> GetByEmail(string email);
        Task<IResult<Client>> GetById(Guid id);
        Task<IResult<string>> Update(Client client);
    }
}