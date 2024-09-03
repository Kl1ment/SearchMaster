using SearchMaster.Core.Enum;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Entities;

namespace SearchMaster.DataAccess
{
    public static class MappingExtensions
    {
        public static PersonEntity MapToEntity(this Person person)
        {
            return new PersonEntity
            {
                Id = person.Id,
                Email = person.Email,
                Name = person.Name,
                Surname = person.Surname,
                Rating = person.Rating,
                Reviews = person.Reviews.Select(r => r.MapToEntity()).ToList(),
                RoleId = (int)person.Role
            };
        }

        public static ClientEntity MapToEntity(this Client client)
        {
            return new ClientEntity
            {
                Id = client.Id,
                Email = client.Email,
                Name = client.Name,
                Surname = client.Surname,
                Rating = client.Rating,
                Reviews = client.Reviews.Select(r => r.MapToEntity()).ToList(),
                Orders = client.Orders.Select(o => o.MapToEntity()).ToList(),
                RoleId = (int)client.Role
            };
        }

        public static WorkerEntity MapToEntity(this Worker worker)
        {
            return new WorkerEntity
            {
                Id = worker.Id,
                Email = worker.Email,
                Username = worker.Username,
                Name = worker.Name,
                Surname = worker.Surname,
                Profession = worker.Profession,
                About = worker.About,
                Rating = worker.Rating,
                Reviews = worker.Reviews.Select(r => r.MapToEntity()).ToList(),
                RoleId = (int)worker.Role
            };
        }

        public static OrderEntity MapToEntity(this Order order)
        {
            return new OrderEntity
            {
                Id = order.Id,
                Client = order.Client?.MapToEntity(),
                ClientId = order.ClientId,
                Title = order.Title,
                Description = order.Description,
                Price = order.Price,
                CreatedDate = order.CreatedDate
            };
        }

        public static ReviewEntity MapToEntity(this Review review)
        {
            return new ReviewEntity
            {
                Id = review.Id,
                Writer = review.Writer?.MapToEntity(),
                Mark = review.Mark,
                TextData = review.TextData,
                CreatedDate = review.CreatedDate,
                Holder = review.Holder?.MapToEntity(),
                WriterId = review.WriterId,
                HolderId = review.HolderId
            };
        }

        public static CodeEntity MapToEntity(this Code code)
        {
            return new CodeEntity
            {
                Id = code.Id,
                CodeHash = code.CodeHash,
            };
        }

        public static Person MapToModel(this PersonEntity personEntity)
        {
            return new Person(
                personEntity.Id,
                personEntity.Email,
                personEntity.Name,
                personEntity.Surname,
                personEntity.Rating,
                personEntity.Reviews.Select(r => { r.Holder = null; return r.MapToModel(); }).ToList(),
                Enum.Parse<Roles>(personEntity.RoleEntity.Name));
        }

        public static Client MapToModel(this ClientEntity clientEntity)
        {
            return Client.Create(
                clientEntity.Id,
                clientEntity.Email,
                clientEntity.Name,
                clientEntity.Surname,
                clientEntity.Rating,
                clientEntity.Reviews.Select(r => { r.Holder = null; return r.MapToModel(); }).ToList(),
                clientEntity.Orders.Select(o =>  { o.Client = null; return o.MapToModel(); }).ToList());
        }

        public static Worker MapToModel(this WorkerEntity workerEntity)
        {
            return Worker.Create(
                workerEntity.Id,
                workerEntity.Email,
                workerEntity.Username,
                workerEntity.Name,
                workerEntity.Surname,
                workerEntity.Profession,
                workerEntity.About,
                workerEntity.Rating,
                workerEntity.Reviews.Select(r => { r.Holder = null; return r.MapToModel(); }).ToList());
        }

        public static Order MapToModel(this OrderEntity orderEntity)
        {
            if (orderEntity.Client != null)
                orderEntity.Client.Orders = [];

            return Order.Create(
                orderEntity.Id,
                orderEntity.Client?.MapToModel(),
                orderEntity.ClientId,
                orderEntity.Title,
                orderEntity.Description,
                orderEntity.Price,
                orderEntity.CreatedDate);
        }

        public static Review MapToModel(this ReviewEntity reviewEntity)
        {
            if (reviewEntity.Holder != null)
                reviewEntity.Holder.Reviews = [];

            return Review.Create(
                reviewEntity.Id,
                reviewEntity.Writer?.MapToModel(),
                reviewEntity.WriterId,
                reviewEntity.Mark,
                reviewEntity.TextData,
                reviewEntity.CreatedDate,
                reviewEntity.Holder?.MapToModel(),
                reviewEntity.HolderId);
        }

        public static Code MapToModel(this CodeEntity codeEntity)
        {
            return new Code(
                codeEntity.Id,
                codeEntity.CodeHash);
        }
    }
}
