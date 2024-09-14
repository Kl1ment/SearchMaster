using SearchMaster.Core.Models;

namespace SearchMaster.Infrastructure
{
    public interface IJwtProvider
    {
        string GenerateLoginToken(string email);
        string GenerateRegistrationToken(string email);
        string GenerateToken(Person person);
    }
}