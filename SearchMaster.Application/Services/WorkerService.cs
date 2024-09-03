using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Repositories;
using SearchMaster.Infrastructure;

namespace SearchMaster.Application.Services
{
    public class WorkerService(
        IWorkerRepository workerRepository,
        IUsernameService usernameService,
        IJwtProvider jwtProvider) : IWorkerService
    {
        private readonly IWorkerRepository _workerRepository = workerRepository;
        private readonly IUsernameService _usernameService = usernameService;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<IResult<(string Username, string Token)>> RegisterWorker(
            string email,
            string name,
            string surname,
            string profession,
            string about)
        {
            var username = await _usernameService.Generate(surname, name);

            var worker = Worker.Create(
                Guid.NewGuid(),
                email,
                username,
                name,
                surname,
                profession,
                about);

            var result = await _workerRepository.Add(worker);

            if (result.IsFailure)
                return Result.Failure<(string Username, string Token)>(result.Error);

            var token = _jwtProvider.GenerateToken(worker);

            return Result.Success((result.Value, token));
        }

        public async Task<IResult<Worker>> GetWorkerByUsername(string username)
        {
            return await _workerRepository.GetByUsername(username);
        }

        public async Task<IResult<Worker>> GetWorkerById(Guid id)
        {
            return await _workerRepository.GetById(id);
        }

        public async Task<IResult<Worker>> GetWorkerByEmail(string email)
        {
            return await _workerRepository.GetByEmail(email);
        }

        public async Task<IResult<string>> UpdateWorker(Guid id, string email, string name, string surname, string profession, string about)
        {
            var worker = await _workerRepository.GetById(id);

            if (worker.IsFailure)
                return Result.Failure<string>(worker.Error);

            var username = worker.Value.Username;

            if (worker.Value.Name != name || worker.Value.Surname != surname)
                username = await _usernameService.Generate(surname, name);

            var newWorker = Worker.Create(
                id,
                email,
                username,
                name,
                surname,
                profession,
                about);

            return await _workerRepository.Update(newWorker);
        }

        public async Task<IResult<string>> DeleteWorker(Guid id)
        {
            return await _workerRepository.Delete(id);
        }
    }
}
