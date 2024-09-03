using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchMaster.Core.Enum;
using SearchMaster.Core.Models;
using SearchMaster.DataAccess.Entities;

namespace SearchMaster.DataAccess.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .HasMany(p => p.Reviews)
                .WithOne(r => r.Holder)
                .HasForeignKey(r => r.HolderId)
                .IsRequired();

            builder
                .Navigation(p => p.RoleEntity)
                .AutoInclude();

            builder
                .Property(p => p.Name)
                .HasMaxLength(Person.MaxLengthString)
                .IsRequired();

            builder
                .Property(p => p.Surname)
                .HasMaxLength(Person.MaxLengthString)
                .IsRequired();

            var admin = new PersonEntity
            {
                Id = Guid.NewGuid(),
                Email = "admin@gmail.com",
                Name = "Климент",
                Surname = "Иванов",
                RoleId = (int)Roles.Admin
            };

            builder.HasData(admin);
        }
    }
}
