using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SearchMaster.Core.Models;

namespace SearchMaster.DataAccess.Repositories
{
    public class CodeRepository(SearchMasterDbContext context) : ICodeRepository
    {
        private readonly SearchMasterDbContext _context = context;

        public async Task<IResult> Add(Code code)
        {
            var codeEntity = code.MapToEntity();

            try
            {
                await _context.Codes.AddAsync(codeEntity);
                await _context.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.ToString());
            }
        }

        public async Task<Code?> Get(Guid id)
        {
            var codeEntity = await _context.Codes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return codeEntity?.MapToModel();
        }

        public async Task DeleteAll()
        {
            await _context.Codes.ExecuteDeleteAsync();
        }
    }
}
