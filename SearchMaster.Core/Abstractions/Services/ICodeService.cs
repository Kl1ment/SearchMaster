using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;

namespace SearchMaster.Application.Services
{
    public interface ICodeService
    {
        Task<IResult<string>> CheckEmailForLogin(Guid codeId, string code, Person person);
        Task<IResult<string>> CheckEmailForRegister(Guid codeId, string code, string email);
        Task<string> SendCode(string email);
    }
}