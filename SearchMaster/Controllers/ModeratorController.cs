using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchMaster.Application.Services;
using SearchMaster.Contracts.Request;
using SearchMaster.Core.Enum;

namespace SearchMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModeratorController(IStaffService staffService) : ControllerBase
    {
        private readonly IStaffService _staffService = staffService;

        [HttpPost]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<ActionResult> AddEmployee(StaffRequest staffRequest)
        {
            var result = await _staffService.AddEmployee(
                staffRequest.Email,
                staffRequest.Name,
                staffRequest.Surname,
                staffRequest.Role);

            if (result.IsFailure)
                return BadRequest(result);

            return Ok(result.Value);
        }

        [HttpPut]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<ActionResult> DeletePerson([FromBody]Guid id)
        {
            var result = await _staffService.DeletePerson(id);

            if (result.IsFailure)
                return BadRequest(result);

            return Ok(result.Value);
        }
    }
}
