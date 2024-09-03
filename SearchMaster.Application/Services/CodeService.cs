using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Repositories;
using SearchMaster.Infrastructure;

namespace SearchMaster.Application.Services
{
    public class CodeService(
        IHasher hasher,
        ICodeRepository codeRepository,
        IJwtProvider jwtProvider,
        IEmailService emailService) : ICodeService
    {
        private readonly IHasher _hasher = hasher;
        private readonly ICodeRepository _codeRepository = codeRepository;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IEmailService _emailService = emailService;

        public async Task<string> SendCode(string email)
        {
            string codeNumber = new Random().Next(1000, 9999).ToString();

            string codeHash = _hasher.Generate(codeNumber);

            Code code = new(Guid.NewGuid(), codeHash);

            string token = _jwtProvider.GenerateLoginToken(code.Id, email);

            await _codeRepository.Add(code);

            Console.WriteLine(codeNumber);
            var htmlMessage = File.ReadAllText(Directory.GetCurrentDirectory() + "/Html/CodeMessage.html");
            await _emailService.SendEmail(email, "Check Email", string.Format(htmlMessage, codeNumber));

            return token;
        }

        public async Task<IResult<string>> CheckEmailForLogin(Guid codeId, string code, Person person)
        {
            var codeHash = await _codeRepository.Get(codeId);

            if (codeHash == null || !_hasher.Verify(code, codeHash.CodeHash))
            {
                return Result.Failure<string>("Wrong code");
            }

            string token = _jwtProvider.GenerateToken(person);

            return Result.Success(token);
        }

        public async Task<IResult<string>> CheckEmailForRegister(Guid codeId, string code, string email)
        {
            var codeHash = await _codeRepository.Get(codeId);

            if (codeHash == null || !_hasher.Verify(code, codeHash.CodeHash))
            {
                return Result.Failure<string>("Wrong code");
            }

            string token = _jwtProvider.GenerateRegistrationToken(email);

            return Result.Success(token);
        }
    }
}
