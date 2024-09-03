using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Repositories;
using SearchMaster.Infrastructure;
using System;

namespace SearchMaster.Application.Services
{
    public class ClientService(IClientRepository clientRepository, IJwtProvider jwtProvider) : IClientService
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<IResult<Client>> GetClientById(Guid id)
        {
            return await _clientRepository.GetById(id);
        }

        public async Task<IResult<Client>> GetClientByEmail(string email)
        {
            return await _clientRepository.GetByEmail(email);
        }

        public async Task<IResult<(Guid Id, string Token)>> RegisterClient(string email, string name, string surname)
        {
            var client = Client.Create(
                Guid.NewGuid(),
                email,
                name,
            surname);

            var result = await _clientRepository.Add(client);

            if (result.IsFailure)
                return Result.Failure<(Guid Id, string Token)>(result.Error);

            string token = _jwtProvider.GenerateToken(client);

            return Result.Success((result.Value, token));
        }

        public async Task<IResult<string>> UpdateClient(Guid id, string email, string name, string surname)
        {
            var client = Client.Create(
                id,
                email,
                name,
                surname);

            return await _clientRepository.Update(client);
        }

        public async Task<IResult<string>> DeleteClient(Guid id)
        {
            return await _clientRepository.Delete(id);
        }
    }
}
