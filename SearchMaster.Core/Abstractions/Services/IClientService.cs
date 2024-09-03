using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;

namespace SearchMaster.Application.Services
{
    public interface IClientService
    {
        Task<IResult<(Guid Id, string Token)>> RegisterClient(string email, string name, string surname);
        Task<IResult<string>> DeleteClient(Guid id);
        Task<IResult<Client>> GetClientByEmail(string email);
        Task<IResult<Client>> GetClientById(Guid id);
        Task<IResult<string>> UpdateClient(Guid id, string email, string name, string surname);
    }
}