using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;

namespace SearchMaster.Application.Services
{
    public interface IOrderService
    {
        Task<IResult<string>> CreateOrder(Guid clientId, string title, string description, decimal price);
        Task<IResult<string>> DeleteOrder(Guid id, string? userId);
        Task<IResult<Order>> GetOrderById(Guid id);
        Task<List<Order>> GetOrdersByParameters(SearchParameters parameters);
        Task<IResult<string>> UpdateOrder(Guid id, string title, string description, decimal price, string? userId);
    }
}