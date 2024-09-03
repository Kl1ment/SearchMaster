using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Repositories
{
    public class OrderRepository(SearchMasterDbContext context) : IOrderRepository
    {
        private const int PageSize = 10;

        private readonly SearchMasterDbContext _context = context;

        public async Task<IResult<string>> Add(Order order)
        {
            try
            {
                var orderEntity = order.MapToEntity();

                await _context.Orders.AddAsync(orderEntity);
                await _context.SaveChangesAsync();

                return Result.Success("Order has been added");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.ToString());
            }
        }

        public async Task<IResult<Order>> GetById(Guid id)
        {
            try
            {
                var orderEntity = await _context.Orders
                    .AsNoTracking()
                    .Where(o => o.Id == id)
                    .FirstOrDefaultAsync();

                if (orderEntity == null)
                    throw new ArgumentNullException("The order was not found");

                return Result.Success(orderEntity.MapToModel());
            }
            catch (ArgumentNullException ex)
            {
                return Result.Failure<Order>(ex.Message);
            }
            catch (Exception ex)
            {
                return Result.Failure<Order>(ex.ToString());
            }

        }

        public async Task<List<Order>> GetByParameters(SearchParameters parameters)
        {

            var request = _context.Orders.AsNoTracking();

            if (!string.IsNullOrEmpty(parameters.Profession))
            {
                request = request.Where(o => o.Title.ToLower().Contains(parameters.Profession.ToLower()) ||
                    o.Description.ToLower().Contains(parameters.Profession.ToLower()));
            }

            if (parameters.MaxPrice > 0)
            {
                request = request.Where(o => o.Price <= parameters.MaxPrice);
            }

            request = request.Where(o => o.Price >= parameters.MinPrice);

            var orderEntities = await request
                .Include(o => o.Client)
                .OrderByDescending(o => o.CreatedDate)
                .Skip((parameters.Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return orderEntities.Select(o => o.MapToModel()).ToList();
        }

        public async Task<IResult<string>> Update(Order order)
        {
            try
            {
                var orderEntity = order.MapToEntity();

                await _context.Orders
                    .Where(o => o.Id == orderEntity.Id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(p => p.Title, p => orderEntity.Title)
                        .SetProperty(p => p.Description, p => orderEntity.Description)
                        .SetProperty(p => p.Price, p => orderEntity.Price)
                        .SetProperty(p => p.CreatedDate, p => orderEntity.CreatedDate));

                await _context.SaveChangesAsync();

                return Result.Success("Changed successfully");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.ToString());
            }
        }

        public async Task<IResult<string>> Delete(Guid id)
        {
            try
            {
                await _context.Orders
                    .Where(o => o.Id == id)
                    .ExecuteDeleteAsync();

                await _context.SaveChangesAsync();

                return Result.Success("Order has been deleted");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(ex.ToString());
            }
        }
    }
}
