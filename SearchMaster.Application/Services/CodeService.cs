using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Distributed;
using SearchMaster.Core.Models;
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
            string code = new Random().Next(1000, 9999).ToString();

            var codeId = Guid.NewGuid().ToString();

            string codeHash = _hasher.Generate(code);

            string token = _jwtProvider.GenerateLoginToken(codeId, email);

            await _cache.SetStringAsync(codeId, codeHash, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            Console.WriteLine(code);
            var htmlMessage = File.ReadAllText(Directory.GetCurrentDirectory() + "/Html/CodeMessage.html");
            await _emailService.SendEmail(email, "Check Email", string.Format(htmlMessage, code));

            return token;
        }

        public async Task<IResult<string>> CheckEmailForLogin(string codeId, string code, Person person)
        {
            var codeHash = await _cache.GetStringAsync(codeId);

            if (codeHash == null || !_hasher.Verify(code, codeHash))
            {
                return Result.Failure<string>("Wrong code");
            }

            await _cache.RemoveAsync(codeId);

            string token = _jwtProvider.GenerateToken(person);

            return Result.Success(token);
        }

        public async Task<IResult<string>> CheckEmailForRegister(string codeId, string code, string email)
        {
            var codeHash = await _cache.GetStringAsync(codeId);

            if (codeHash == null || !_hasher.Verify(code, codeHash))
            {
                return Result.Failure<string>("Wrong code");
            }

            await _cache.RemoveAsync(codeId);

            string token = _jwtProvider.GenerateRegistrationToken(email);

            return Result.Success(token);
        }
    }
}
