using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Entities;

namespace SearchMaster.DataAccess.Configurations
{
    public class WorkerConfiguration : IEntityTypeConfiguration<WorkerEntity>
    {
        public void Configure(EntityTypeBuilder<WorkerEntity> builder)
        {
            builder
                .Property(w => w.About)
                .HasMaxLength(Worker.MaxAboutLength)
                .IsRequired();
        }
    }
}
