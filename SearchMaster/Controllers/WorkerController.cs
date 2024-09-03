using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchMaster.Application.Services;
using SearchMaster.Contracts.Request;
using SearchMaster.Contracts.Response;
using SearchMaster.Core;
using SearchMaster.Extensions;
using System.Security.Claims;

namespace SearchMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkerController(IWorkerService workerService) : ControllerBase
    {
        private readonly IWorkerService _workerService = workerService;

        [HttpGet]
        [Route("{username:alpha}")]
        public async Task<ActionResult<WorkerResponse>> GetWorkerByUsername(string username)
        {
            var worker = await _workerService.GetWorkerByUsername(username);

            if (worker.IsFailure)
                return BadRequest(worker.Error);

            return worker.Value.MapToResponse();
        }

        [HttpGet]
        public async Task<ActionResult<WorkerResponse>> GetWorkerById([FromQuery] Guid workerId)
        {
            var worker = await _workerService.GetWorkerById(workerId);

            if (worker.IsFailure)
                return BadRequest(worker.Error);

            return worker.Value.MapToResponse();
        }

        [HttpPut]
        [Route("Settings")]
        [Authorize(Policy = ApiExtensions.WorkerModeratorPolicy)]
        public async Task<ActionResult> UpdateWorker([FromBody] WorkerUpdateRequest worker)
        {
            if (Guid.TryParse(HttpContext.User.FindFirstValue(Strings.UserId), out Guid workerId))
            {
                var result = await _workerService.UpdateWorker(workerId, worker.Email, worker.Name, worker.Surname, worker.Profession, worker.About);

                if (result.IsFailure)
                    return BadRequest(result.Error);
                return Ok(result.Value);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("Settings")]
        [Authorize(Policy = ApiExtensions.WorkerModeratorPolicy)]
        public async Task<ActionResult> DeleteWorker()
        {
            if (Guid.TryParse(HttpContext.User.FindFirstValue(Strings.UserId), out Guid workerId))
            {
                var result = await _workerService.DeleteWorker(workerId);

                if (result.IsFailure)
                    return BadRequest(result.Error);

                return Ok(result.Value);
            }

            return BadRequest();
        }
    }
}
