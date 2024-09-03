using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;

namespace SearchMaster.Application.Services
{
    public interface IWorkerService
    {
        Task<IResult<(string Username, string Token)>> RegisterWorker(string email, string name, string surname, string profession, string about);
        Task<IResult<string>> DeleteWorker(Guid id);
        Task<IResult<Worker>> GetWorkerByUsername(string username);
        Task<IResult<Worker>> GetWorkerByEmail(string email);
        Task<IResult<Worker>> GetWorkerById(Guid id);
        Task<IResult<string>> UpdateWorker(Guid id, string email, string name, string surname, string profession, string about);
    }
}