using Microsoft.EntityFrameworkCore;
using SearchMaster.DataAccess.Configurations;
using SearchMaster.DataAccess.Entities;

namespace SearchMaster.DataAccess
{
    public class SearchMasterDbContext(DbContextOptions<SearchMasterDbContext> options) : DbContext(options)
    {
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<WorkerEntity> Workers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
