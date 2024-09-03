using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchMaster.DataAccess.Entities;

namespace SearchMaster.DataAccess.Configurations
{
    public class CodeConfiguration : IEntityTypeConfiguration<CodeEntity>
    {
        public void Configure(EntityTypeBuilder<CodeEntity> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
