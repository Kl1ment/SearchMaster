using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Entities;

namespace SearchMaster.DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(o => o.Id);

            builder
                .Property(o => o.Title)
                .HasMaxLength(Order.MaxTitleLength)
                .IsRequired();

            builder
                .Property(o => o.Description)
                .HasMaxLength(Order.MaxDescriptionLength);
        }
    }
}
