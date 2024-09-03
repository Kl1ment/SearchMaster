using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchMaster.Core.Enum;
using SearchMaster.DataAccess.Entities;

namespace SearchMaster.DataAccess.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(r => r.Id);

            builder
                .HasMany(r => r.Persons)
                .WithOne(p => p.RoleEntity)
                .HasForeignKey(p => p.RoleId)
                .IsRequired();

            var roles = Enum.GetValues<Roles>()
                .Select(r => new RoleEntity
                {
                    Id = (int)r,
                    Name = r.ToString(),
                });

            builder.HasData(roles);
        }
    }
}
