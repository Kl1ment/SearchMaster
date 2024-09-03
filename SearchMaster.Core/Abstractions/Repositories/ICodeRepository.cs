using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Repositories
{
    public interface ICodeRepository
    {
        Task<IResult> Add(Code code);
        Task DeleteAll();
        Task<Code?> Get(Guid id);
    }
}