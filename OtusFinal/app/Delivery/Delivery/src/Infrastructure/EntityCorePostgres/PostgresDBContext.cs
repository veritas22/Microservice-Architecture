using Entity;
using EntityCorePostgres.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EntityCorePostgres
{
    public class PostgresDBContext : DbContext
    {
        public DbSet<ReserveCourier> ReserveCourier { get; set; }


        public PostgresDBContext(DbContextOptions<PostgresDBContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ReserveCourierConfiguration());

        }
    }
}
