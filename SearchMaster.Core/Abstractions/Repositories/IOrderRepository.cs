using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Repositories
{
    public interface IOrderRepository
    {
        Task<IResult<string>> Add(Order order);
        Task<IResult<string>> Delete(Guid id);
        Task<IResult<Order>> GetById(Guid id);
        Task<List<Order>> GetByParameters(SearchParameters parameters);
        Task<IResult<string>> Update(Order order);
    }
}