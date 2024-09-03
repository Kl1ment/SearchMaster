using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SearchMaster.Core.Models;
using System.Text.RegularExpressions;

namespace SearchMaster.DataAccess.Repositories
{
    public class WorkerRepository(SearchMasterDbContext context) : IWorkerRepository
    {
        private readonly SearchMasterDbContext _context = context;

        public async Task<IResult<string>> Add(Worker worker)
        {
            try
            {
                var workerEntity = worker.MapToEntity();

                await _context.Workers.AddAsync(workerEntity);
                await _context.SaveChangesAsync();

                return Result.Success(worker.Username);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.ToString());
            }
        }

        public async Task<IResult<Worker>> GetByUsername(string username)
        {
            var workerEntity = await _context.Workers
                .AsNoTracking()
                .Include(w => w.Reviews)
                .FirstOrDefaultAsync(w => w.Username == username);

            if (workerEntity == null)
                return Result.Failure<Worker>("The account was not found");

            var worker = workerEntity.MapToModel();

            return Result.Success(worker);
        }

        public async Task<IResult<Worker>> GetByEmail(string email)
        {
            var workerEntity = await _context.Workers
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Email == email);

            if (workerEntity == null)
                return Result.Failure<Worker>("The account was not found");

            var worker = workerEntity.MapToModel();

            return Result.Success(worker);
        }

        public async Task<IResult<Worker>> GetById(Guid id)
        {
            var workerEntity = await _context.Workers
                .AsNoTracking()
                .Include(w => w.Reviews)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (workerEntity == null)
                return Result.Failure<Worker>("The account was not found");

            var worker = workerEntity.MapToModel();

            return Result.Success(worker);
        }

        public async Task<IResult<string>> Update(Worker worker)
        {
            try
            {
                var workerEntity = worker.MapToEntity();

                await _context.Workers
                    .Where(w => w.Id == workerEntity.Id)
                    .ExecuteUpdateAsync(p => p
                        .SetProperty(c => c.Email, workerEntity.Email)
                        .SetProperty(c => c.Name, workerEntity.Name)
                        .SetProperty(c => c.Surname, workerEntity.Surname)
                        .SetProperty(c => c.Profession, workerEntity.Profession)
                        .SetProperty(c => c.About, workerEntity.About));

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
                await _context.Workers
                    .Where(w => w.Id == id)
                    .ExecuteDeleteAsync();

                await _context.SaveChangesAsync();

                return Result.Success("Account has been deleted");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.ToString());
            }
        }

        public async Task<int?> GetMaxUsernameNumber(string username)
        {
            var usernames = await _context.Workers
                .AsNoTracking()
                .Where(p => Regex.IsMatch(p.Username, @$"^{username}\d*"))
                .Select(p => p.Username)
                .ToListAsync();

            if (usernames.Count == 0)
                return null;

            return usernames.Max(GetNumber);
        }

        private int GetNumber(string username)
        {
            if (int.TryParse(Regex.Match(username, @"\d+").ToString(), out int number))
                return number;
            else
                return 0;
        }
    }
}
