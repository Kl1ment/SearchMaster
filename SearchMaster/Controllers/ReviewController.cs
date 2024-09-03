using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchMaster.Application.Services;
using SearchMaster.Contracts.Request;
using SearchMaster.Core;
using SearchMaster.Extensions;
using System.Security.Claims;

namespace SearchMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {
        private readonly IReviewService _reviewService = reviewService;

        [HttpPost]
        [Authorize(Policy = ApiExtensions.ClientWorkerModeratorPolicy)]
        public async Task<ActionResult> CreateReview(ReviewRequest reviewRequest)
        {
            var userId = HttpContext.User.FindFirstValue(Strings.UserId);

            var result = await _reviewService.CreateReview(
                userId,
                reviewRequest.Mark,
                reviewRequest.TextData,
                reviewRequest.HolderId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete]
        [Authorize(Policy = ApiExtensions.ClientWorkerModeratorPolicy)]
        public async Task<ActionResult> DeleteReview(Guid reviewId)
        {
            var removerId = HttpContext.User.FindFirstValue(Strings.UserId);

            var result = await _reviewService.DeleteReview(reviewId, removerId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
