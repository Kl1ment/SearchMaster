using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Distributed;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Repositories;
using SearchMaster.Infrastructure;

namespace SearchMaster.Application.Services
{
    public class CodeService(
        IHasher hasher,
        IDistributedCache cache,
        IJwtProvider jwtProvider,
        IEmailService emailService) : ICodeService
    {
        private readonly IHasher _hasher = hasher;
        private readonly IDistributedCache _cache = cache;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IEmailService _emailService = emailService;

        public async Task<string> SendCode(string email)
        {
            string codeNumber = new Random().Next(1000, 9999).ToString();

            string codeHash = _hasher.Generate(codeNumber);

            string token = _jwtProvider.GenerateLoginToken(email);

            await _cache.SetStringAsync(email, codeHash, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            Console.WriteLine(codeNumber);
            var htmlMessage = File.ReadAllText(Directory.GetCurrentDirectory() + "/Html/CodeMessage.html");
            await _emailService.SendEmail(email, "Check Email", string.Format(htmlMessage, codeNumber));

            return token;
        }

        public async Task<IResult<string>> CheckEmailForLogin(string email, string code, Person person)
        {
            var codeHash = await _cache.GetStringAsync(email);

            if (codeHash == null || !_hasher.Verify(code, codeHash))
            {
                return Result.Failure<string>("Wrong code");
            }

            string token = _jwtProvider.GenerateToken(person);

            return Result.Success(token);
        }

        public async Task<IResult<string>> CheckEmailForRegister(string code, string email)
        {
            var codeHash = await _cache.GetStringAsync(email);

            if (codeHash == null || !_hasher.Verify(code, codeHash))
            {
                return Result.Failure<string>("Wrong code");
            }

            string token = _jwtProvider.GenerateRegistrationToken(email);

            return Result.Success(token);
        }
    }
}
