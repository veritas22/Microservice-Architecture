using Domen;
using Domen.Aggregate;
using EntityCorePostgres.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EntityCorePostgres
{
    public class PostgresDBContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<EventsOrder> EventsOrder { get; set; }


        public PostgresDBContext(DbContextOptions<PostgresDBContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new EventsOrderConfiguration());

        }
    }
}
