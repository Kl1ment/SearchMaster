using SearchMaster.Contracts.Response;
using SearchMaster.Core.Models;

namespace SearchMaster.Extensions
{
    public static class MappingExtensions
    {
        public static PersonResponse MapToResponse(this Person person)
        {
            return new PersonResponse(
                person.Id,
                person.Name,
                person.Surname);
        }

        public static ClientResponse MapToResponse(this Client client)
        {
            return new ClientResponse(
                client.Id,
                client.Name,
                client.Surname,
                client.Rating,
                client.Reviews.Select(r => r.MapToResponse()).ToList(),
                client.Orders.Select(o => o.MapToResponse()).ToList());
        }

        public static WorkerResponse MapToResponse(this Worker worker)
        {
            return new WorkerResponse(
                worker.Id,
                worker.Username,
                worker.Name,
                worker.Surname,
                worker.Profession,
                worker.About,
                worker.Rating,
                worker.Reviews.Select(r => r.MapToResponse()).ToList());
        }

        public static OrderResponse MapToResponse(this Order order)
        {
            return new OrderResponse(
                order.Id,
                order.Client?.MapToResponse() ?? default,
                order.Title,
                order.Description,
                order.Price,
                order.CreatedDate);
        }

        public static ReviewResponse MapToResponse(this Review review)
        {
            return new ReviewResponse(
                review.Writer?.MapToResponse() ?? default,
                review.Mark,
                review.TextData,
                review.CreatedDate);
        }
    }
}
