using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchMaster.Application.Services;
using SearchMaster.Contracts.Request;
using SearchMaster.Contracts.Response;
using SearchMaster.Core;
using SearchMaster.Core.Enum;
using SearchMaster.Extensions;
using System.Security.Claims;

namespace SearchMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = nameof(Roles.Client))]
    public class ClientController(IClientService clientService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;

        [HttpGet]
        public async Task<ActionResult<ClientResponse>> GetClientById([FromQuery] Guid clientId)
        {
            var client = await _clientService.GetClientById(clientId);

            if (client.IsFailure)
                return BadRequest(client.Error);

            return client.Value.MapToResponse();
        }

        [HttpPut]
        [Route("Settings")]
        [Authorize(Policy = ApiExtensions.ClientModeratorPolicy)]
        public async Task<ActionResult> UpdateClient(ClientUpdateRequest client)
        {
            if (Guid.TryParse(HttpContext.User.FindFirstValue(Strings.UserId), out Guid clientId))
            {
                var result = await _clientService.UpdateClient(clientId, client.Email, client.Name, client.Surname);

                if (result.IsFailure)
                    return BadRequest(result.Error);

                return Ok(result.Value);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("Settings")]
        [Authorize(Policy = ApiExtensions.ClientModeratorPolicy)]
        public async Task<ActionResult> DeleteClient()
        {
            if (Guid.TryParse(HttpContext.User.FindFirstValue(Strings.UserId), out Guid clientId))
            {
                var result = await _clientService.DeleteClient(clientId);

                if (result.IsFailure)
                    return BadRequest(result.Error);

                return Ok(result.Value);
            }

            return BadRequest();
        }
    }
}
