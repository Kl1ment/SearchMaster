using CSharpFunctionalExtensions;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Repositories;

namespace SearchMaster.Application.Services
{
    public class OrderService(IOrderRepository orderRepository, IPersonRepository personRepository) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IPersonRepository _personRepository = personRepository;

        public async Task<IResult<Order>> GetOrderById(Guid id)
        {
            return await _orderRepository.GetById(id);
        }

        public async Task<List<Order>> GetOrdersByParameters(SearchParameters searchParameters)
        {
            return await _orderRepository.GetByParameters(searchParameters);
        }

        public async Task<IResult<string>> CreateOrder(Guid clientId, string title, string description, decimal price)
        {
            var order = Order.Create(
                Guid.NewGuid(),
                default,
                clientId,
                title,
                description,
                price,
                DateTime.UtcNow);

            return await _orderRepository.Add(order);
        }

        public async Task<IResult<string>> UpdateOrder(Guid id, string title, string description, decimal price, string? userId)
        {
            var permissionChange = await CheckPermissionToChange(id, userId);

            if (permissionChange.IsFailure)
                return permissionChange;

            var order = Order.Create(
                id,
                default,
                default,
                title,
                description,
                price,
                DateTime.UtcNow);

            return await _orderRepository.Update(order);
        }

        public async Task<IResult<string>> DeleteOrder(Guid id, string? userId)
        {
            var permissionChange = await CheckPermissionToChange(id, userId);

            if (permissionChange.IsFailure)
                return permissionChange;

            return await _orderRepository.Delete(id);
        }

        private async Task<IResult<string>> CheckPermissionToChange(Guid id, string? userId)
        {
            string error = "You are not the order owner";

            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return Result.Failure<string>(error);

            var order = await _orderRepository.GetById(id);

            if (order.IsFailure)
                return Result.Failure<string>(order.Error);

            if (order.Value.ClientId != userIdGuid)
            {
                var personRole = await _personRepository.GetRole(userIdGuid);

                if (personRole.IsFailure)
                    return Result.Failure<string>(error);

                if (personRole.Value != Core.Enum.Roles.Admin)
                    return Result.Failure<string>(error);
            }

            return Result.Success(string.Empty);
        }
    }
}
