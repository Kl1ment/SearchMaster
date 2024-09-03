using CSharpFunctionalExtensions;

namespace SearchMaster.Application.Services
{
    public interface IStaffService
    {
        Task<IResult<string>> AddEmployee(string email, string name, string surname, string role);
        Task<IResult<string>> DeletePerson(Guid id);
    }
}