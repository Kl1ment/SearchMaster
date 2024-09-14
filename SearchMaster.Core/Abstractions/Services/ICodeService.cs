using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;

namespace SearchMaster.Application.Services
{
    public interface ICodeService
    {
        Task<IResult<string>> CheckEmailForLogin(string email, string code, Person person);
        Task<IResult<string>> CheckEmailForRegister(string code, string email);
        Task<string> SendCode(string email);
    }
}