using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;

namespace SearchMaster.Application.Services
{
    public interface IReviewService
    {
        Task<IResult<string>> CreateReview(string? writerId, int mark, string textData, Guid holderId);
        Task<IResult<string>> DeleteReview(Guid id, string? removerId);
        Task<List<Review>> GetByHolderId(Guid id);
    }
}