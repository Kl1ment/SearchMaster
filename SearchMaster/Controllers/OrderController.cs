using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchMaster.Application.Services;
using SearchMaster.Contracts.Request;
using SearchMaster.Contracts.Response;
using SearchMaster.Core;
using SearchMaster.Core.Enum;
using SearchMaster.Core.Models;
using SearchMaster.Extensions;
using System.Security.Claims;

namespace SearchMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpPost]
        [Route("Search")]
        [Authorize(Policy = ApiExtensions.WorkerModeratorPolicy)]
        public async Task<ActionResult<List<OrderResponse>>> GetOrdersByParameters(SearchParameters searchParameters)
        {
            var orders = await _orderService.GetOrdersByParameters(searchParameters);

            return orders.Select(o => o.MapToResponse()).ToList();
        }

        [HttpGet]
        [Authorize(Policy = ApiExtensions.WorkerModeratorPolicy)]
        public async Task<ActionResult<OrderResponse>> GetOrderById([FromQuery] Guid orderId)
        {
            var result = await _orderService.GetOrderById(orderId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return result.Value.MapToResponse();
        } 

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = nameof(Roles.Client))]
        public async Task<ActionResult> CreateOrder(OrderRequest orderRequest)
        {
            if (Guid.TryParse(HttpContext.User.FindFirstValue(Strings.UserId), out Guid clientId))
            {
                var result = await _orderService.CreateOrder(
                    clientId,
                    orderRequest.Title,
                    orderRequest.Description,
                    orderRequest.Price);

                if (result.IsFailure)
                    return BadRequest(result.Error);

                return Ok(result.Value);
            }

            return BadRequest();
        }

        [HttpPut]
        [Authorize(Policy = ApiExtensions.ClientModeratorPolicy)]
        public async Task<ActionResult> UpdateOrder([FromQuery] Guid orderId, OrderUpdateRequest order)
        {
            var userId = HttpContext.User.FindFirstValue(Strings.UserId);

            var result = await _orderService.UpdateOrder(orderId, order.Title, order.Description, order.Price, userId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete]
        [Authorize(Policy = ApiExtensions.ClientModeratorPolicy)]
        public async Task<ActionResult> DeleteOrder([FromQuery] Guid orderId)
        {
            var userId = HttpContext.User.FindFirstValue(Strings.UserId);

            var result = await _orderService.DeleteOrder(orderId, userId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
