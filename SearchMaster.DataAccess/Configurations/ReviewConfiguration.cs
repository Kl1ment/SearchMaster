using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Entities;

namespace SearchMaster.DataAccess.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
    {
        public void Configure(EntityTypeBuilder<ReviewEntity> builder)
        {
            builder.HasKey(r => r.Id);

            builder
                .Navigation(r => r.Writer)
                .AutoInclude();

            builder
                .Property(r => r.TextData)
                .HasMaxLength(Review.MaxTextLength)
                .IsRequired();
        }
    }
}
