using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Repositories
{
    public class ClientRepository(SearchMasterDbContext context) : IClientRepository
    {
        private readonly SearchMasterDbContext _context = context;

        public async Task<List<Client>> Get()
        {
            var clientEntities = await _context.Clients
                .AsNoTracking()
                .Include(c => c.Orders.OrderByDescending(o => o.CreatedDate))
                .ToListAsync();

            return clientEntities.Select(c => c.MapToModel()).ToList();
        }

        public async Task<IResult<Guid>> Add(Client client)
        {
            try
            {
                var clientEntity = client.MapToEntity();

                await _context.Clients.AddAsync(clientEntity);
                await _context.SaveChangesAsync();

                return Result.Success(clientEntity.Id);
            }
            catch (Exception ex)
            {
                return Result.Failure<Guid>(ex.ToString());
            }
        }

        public async Task<IResult<Client>> GetById(Guid id)
        {
            var clientEntity = await _context.Clients
                .AsNoTracking()
                .Include(w => w.Reviews)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (clientEntity == null)
                return Result.Failure<Client>("The account was not found");

            var client = clientEntity.MapToModel();

            return Result.Success(client);
        }

        public async Task<IResult<Client>> GetByEmail(string email)
        {
            var clientEntity = await _context.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == email);

            if (clientEntity == null)
                return Result.Failure<Client>("The account was not found");

            var client = clientEntity.MapToModel();

            return Result.Success(client);
        }

        public async Task<IResult<string>> Update(Client client)
        {
            try
            {
                var clientEntity = client.MapToEntity();

                await _context.Clients
                    .Where(c => c.Id == clientEntity.Id)
                    .ExecuteUpdateAsync(p => p
                        .SetProperty(c => c.Email, clientEntity.Email)
                        .SetProperty(c => c.Name, clientEntity.Name)
                        .SetProperty(c => c.Surname, clientEntity.Surname));

                await _context.SaveChangesAsync();

                return Result.Success("Changed successfully");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.ToString());
            }
        }

        public async Task<IResult<string>> Delete(Guid id)
        {
            try
            {
                await _context.Clients
                    .Where(c => c.Id == id)
                    .ExecuteDeleteAsync();

                await _context.SaveChangesAsync();

                return Result.Success("Account has been deleted");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.ToString());
            }
        }
    }
}
