using Application.Interfaces;
using Domain;
using EntityCorePostgres.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EntityCorePostgres
{
    public class PostgresDBContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Reserve> Reserve { get; set; }


        public PostgresDBContext(DbContextOptions<PostgresDBContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
