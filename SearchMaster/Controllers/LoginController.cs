using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchMaster.Application.Services;
using SearchMaster.Core;
using SearchMaster.Core.Enum;
using System.Security.Claims;

namespace SearchMaster.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class LoginController(
        IClientService clientService,
        IWorkerService workerService,
        ICodeService emailService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;
        private readonly IWorkerService _workerService = workerService;
        private readonly ICodeService _emailService = emailService;

        [HttpPost]
        public async Task<ActionResult> ClientLogin([FromBody] string email)
        {
            var client = await _clientService.GetClientByEmail(email);

            if (client.IsFailure)
                return BadRequest(client.Error);

            var token = await _emailService.SendCode(email);

            HttpContext.Response.Cookies.Append("asp", token.ToString());

            return Redirect(nameof(ClientConfirmEmail));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.ConfirmingEmail))]
        public async Task<ActionResult> ClientConfirmEmail([FromBody] string code)
        {
            string? email = HttpContext.User.FindFirstValue(Strings.Email);
            string? codeId = HttpContext.User.FindFirstValue(Strings.CodeId);

            if (email == null || codeId == null)
                return BadRequest("Wrong Code");

            var client = await _clientService.GetClientByEmail(email);

            if (client.IsFailure)
                return BadRequest(client.Error);

            var token = await _emailService.CheckEmailForLogin(codeId, code, client.Value);

            if (token.IsFailure)
                return BadRequest(token.Error);

            HttpContext.Response.Cookies.Append(Strings.Cookie, token.Value);

            return Redirect($"Client/?clientId={client.Value.Id}");
        }

        [HttpPost]
        public async Task<ActionResult> WorkerLogin([FromBody] string email)
        {
            var client = await _workerService.GetWorkerByEmail(email);

            if (client.IsFailure)
                return BadRequest(client.Error);

            var token = await _emailService.SendCode(email);

            HttpContext.Response.Cookies.Append(Strings.Cookie, token.ToString());

            return Redirect(nameof(WorkerConfirmEmail));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.ConfirmingEmail))]
        public async Task<ActionResult> WorkerConfirmEmail([FromBody] string code)
        {
            string? email = HttpContext.User.FindFirstValue(Strings.Email);
            string? codeId = HttpContext.User.FindFirstValue(Strings.CodeId);

            if (email == null || codeId == null)
                return BadRequest("Wrong Code");

            var worker = await _workerService.GetWorkerByEmail(email);

            if (worker.IsFailure)
                return BadRequest(worker.Error);

            var token = await _emailService.CheckEmailForLogin(codeId, code, worker.Value);

            if (token.IsFailure)
                return BadRequest(token.Error);

            HttpContext.Response.Cookies.Append(Strings.Cookie, token.Value);

            return Redirect($"Worker/{worker.Value.Username}");
        }
    }
}
