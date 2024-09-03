using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchMaster.Application.Services;
using SearchMaster.Contracts.Response;
using SearchMaster.DataAccess;
using SearchMaster.DataAccess.Repositories;
using SearchMaster.Extensions;

namespace SearchMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController(
        IClientRepository clientRepository,
        IPersonRepository personRepository,
        IWorkerRepository workerRepository,
        IEmailService emailService,
        SearchMasterDbContext context) : ControllerBase
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IPersonRepository _personRepository = personRepository;
        private readonly IWorkerRepository _workerRepository = workerRepository;
        private readonly IEmailService _emailService = emailService;
        private readonly SearchMasterDbContext _context = context;

        [HttpGet("GetOrders")]
        public async Task<ActionResult> Index(SearchMasterDbContext context)
        {
            var person = await context.Orders
                .AsNoTracking()
                .Include(p => p.Client)
                .ToListAsync();

            return Ok(person.Select(p => p.MapToModel()).ToList());
        }

        [HttpGet("GetClients")]
        public async Task<ActionResult<List<ClientResponse>>> GetAllClients()
        {
            var req = await _clientRepository.Get();

            return req.Select(c => c.MapToResponse()).ToList();
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetRole(Guid id)
        {
            var result = await _personRepository.GetRole(id);

            return Ok(result.Value);
        }

        [HttpGet("GetWorkers")]
        public async Task<List<WorkerResponse>> GeAllWorkers()
        {
            var person = await _context.Workers
                .AsNoTracking()
                .Include(w => w.Reviews)
                .ToListAsync();

            return person.Select(p => p.MapToModel().MapToResponse()).ToList();
        }

        [HttpGet("GetReviews")]
        public async Task<ActionResult> GeAllReviews()
        {
            var person = await _context.Reviews
                .AsNoTracking()
                .ToListAsync();

            return Ok(person.Select(p => p.MapToModel().MapToResponse()).ToList());
        }

        [HttpGet("GetPersons")]
        public async Task<ActionResult> GeAllPersons()
        {
            var person = await _context.Persons
                .AsNoTracking()
                .ToListAsync();

            return Ok(person.Select(p => p.MapToModel()));
        }

        [HttpPost("Email")]
        public async Task<ActionResult> SendEmail()
        {
            var email = "perevalovdn7@gmail.com";
            var subject = "Тестовая тема";
            var message = "Тестовое сообщение";

            await _emailService.SendEmail(email, subject, message);

            return Ok();
        }
    }
}
