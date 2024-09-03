using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Repositories;

namespace SearchMaster.Application.Services
{
    public class ReviewService(IReviewRepository reviewRepository, IPersonRepository personRepository) : IReviewService
    {
        private readonly IReviewRepository _reviewRepository = reviewRepository;
        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<IResult<string>> CreateReview(string? writerId, int mark, string textData, Guid holderId)
        {
            if (!Guid.TryParse(writerId, out Guid writerIdGuid))
                return Result.Failure<string>("You are not authorize");

            var review = Review.Create(
                Guid.NewGuid(),
                default,
                writerIdGuid,
                mark,
                textData,
                DateTime.UtcNow,
                default,
                holderId);

            return await _reviewRepository.Add(review);
        }

        public async Task<List<Review>> GetByHolderId(Guid id)
        {
            return await _reviewRepository.GetByHolderId(id);
        }

        public async Task<IResult<string>> DeleteReview(Guid id, string? removerId)
        {
            var permissionChange = await CheckPermissionToChange(id, removerId);

            if (permissionChange.IsFailure)
                return Result.Failure<string>(permissionChange.Error);

            return await _reviewRepository.Delete(id);
        }

        private async Task<IResult<string>> CheckPermissionToChange(Guid id, string? removerId)
        {
            string error = "You are not the review writer";

            if (!Guid.TryParse(removerId, out Guid removerIdGuid))
                return Result.Failure<string>(error);

            var review = await _reviewRepository.GetById(id);

            if (review.IsFailure)
                return Result.Failure<string>(review.Error);

            if (review.Value.WriterId != removerIdGuid)
            {
                var removerRole = await _personRepository.GetRole(removerIdGuid);

                if (removerRole.IsFailure)
                    return Result.Failure<string>(error);

                if (removerRole.Value != Core.Enum.Roles.Admin)
                    return Result.Failure<string>(error);
            }

            return Result.Success(string.Empty);
        }
    }
}
