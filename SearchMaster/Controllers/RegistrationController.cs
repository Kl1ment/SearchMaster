using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchMaster.Application.Services;
using SearchMaster.Contracts.Registration;
using SearchMaster.Core;
using SearchMaster.Core.Enum;
using System.Security.Claims;

namespace SearchMaster.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class RegistrationController(
        IClientService clientService,
        IWorkerService workerService,
        IUsernameService usernameService,
        ICodeService emailService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;
        private readonly IWorkerService _workerService = workerService;
        private readonly IUsernameService _usernameService = usernameService;
        private readonly ICodeService _emailService = emailService;

        [HttpPost]
        public async Task<ActionResult> ClientRegistration([FromBody] string email)
        {
            var client = await _clientService.GetClientByEmail(email);

            if (client.IsSuccess)
                return BadRequest("The client with this email is already registered");

            var token = await _emailService.SendCode(email);

            HttpContext.Response.Cookies.Append(Strings.Cookie, token);

            return Redirect(nameof(ConfirmClientEmail));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.ConfirmingEmail))]
        public async Task<ActionResult> ConfirmClientEmail([FromBody] string code)
        {
            var result = await ConfirmEmail(code);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Redirect(nameof(ClientRegistrationNext));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.Registering))]
        public async Task<ActionResult> ClientRegistrationNext([FromBody] ClientRegistrationForm form)
        {
            string? email = HttpContext.User.FindFirstValue(Strings.Email);

            if (email == null)
                return BadRequest();

            var newClient = await _clientService.RegisterClient(email, form.Name, form.Surname);

            if (newClient.IsFailure)
                return BadRequest(newClient.Error);

            HttpContext.Response.Cookies.Append(Strings.Cookie, newClient.Value.Token);

            return Redirect($"Client/?clientId={newClient.Value.Id}");
        }

        [HttpPost]
        public async Task<ActionResult> WorkerRegistration([FromBody] string email)
        {
            var worker = await _workerService.GetWorkerByEmail(email);

            if (worker.IsSuccess)
                return BadRequest("The worker with this email is already registered");

            var token = await _emailService.SendCode(email);

            HttpContext.Response.Cookies.Append(Strings.Cookie, token);

            return Redirect(nameof(ConfirmWorkerEmail));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.ConfirmingEmail))]
        public async Task<ActionResult> ConfirmWorkerEmail([FromBody] string code)
        {
            var result = await ConfirmEmail(code);

            if (result.IsFailure)
                return BadRequest(result); 

            return Redirect(nameof(WorkerRegistrationNext));
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.Registering))]
        public async Task<ActionResult> WorkerRegistrationNext([FromBody] WorkerRegistrationForm form)
        {
            string? email = HttpContext.User.FindFirstValue(Strings.Email);

            if (email == null)
                return BadRequest();

            var newWorker = await _workerService.RegisterWorker(email, form.Name, form.Surname, form.Profession, form.About);

            if (newWorker.IsFailure)
                return BadRequest(newWorker.Error);

            HttpContext.Response.Cookies.Append(Strings.Cookie, newWorker.Value.Token);

            return Redirect($"Worker/{newWorker.Value.Username}");
        }

        private async Task<Result> ConfirmEmail(string code)
        {
            string? email = HttpContext.User.FindFirstValue(Strings.Email);

            if (email == null)
                return Result.Failure("Wrong Code");

            var result = await _emailService.CheckEmailForRegister(code, email);

            if (result.IsFailure)
                return Result.Failure(result.Error);

            HttpContext.Response.Cookies.Append(Strings.Cookie, result.Value);

            return Result.Success();
        }
    }
}
