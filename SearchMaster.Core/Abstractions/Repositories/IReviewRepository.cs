using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Repositories
{
    public interface IReviewRepository
    {
        Task<IResult<string>> Add(Review review);
        Task<IResult<string>> Delete(Guid id);
        Task<IResult<Review>> GetById(Guid id);
        Task<List<Review>> GetByHolderId(Guid holderId);
    }
}