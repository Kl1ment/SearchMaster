using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Repositories
{
    public class ReviewRepository(SearchMasterDbContext context) : IReviewRepository
    {
        private readonly SearchMasterDbContext _context = context;

        public async Task<IResult<string>> Add(Review review)
        {
            try
            {
                var reviewEntity = review.MapToEntity();

                await _context.Reviews.AddAsync(reviewEntity);
                await _context.SaveChangesAsync();

                return Result.Success("Order has been added");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.ToString());
            }
        }

        public async Task<List<Review>> GetByHolderId(Guid holderId)
        {
            var reviewEntities = await _context.Reviews
                .AsNoTracking()
                .Where(r => r.HolderId == holderId)
                .ToListAsync();

            return reviewEntities.Select(r => r.MapToModel()).ToList();
        }

        public async Task<IResult<string>> Delete(Guid id)
        {
            try
            {
                await _context.Reviews
                    .Where(r => r.Id == id)
                    .ExecuteDeleteAsync();

                await _context.SaveChangesAsync();

                return Result.Success("Review has been deleted");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.ToString());
            }
        }

        public async Task<IResult<Review>> GetById(Guid id)
        {
            var reviewEntity = await _context.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reviewEntity == null)
                return Result.Failure<Review>("The review was not found");

            return Result.Success(reviewEntity.MapToModel());
        }
    }
}
