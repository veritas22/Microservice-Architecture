using Entity.User;
using EntityCorePostgres.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EntityCorePostgres
{
    public class PostgresDBContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }


        public PostgresDBContext(DbContextOptions<PostgresDBContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());

        }
    }
}
