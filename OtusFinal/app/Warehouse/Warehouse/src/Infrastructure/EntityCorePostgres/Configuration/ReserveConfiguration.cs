using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace EntityCorePostgres.Configuration
{
    class ReserveConfiguration : IEntityTypeConfiguration<Reserve>
    {
        public void Configure(EntityTypeBuilder<Reserve> builder)
        {
            builder.ToTable("reserve");

            builder.HasKey(x => x.Id);

            builder
                .Property(b => b.ProductId)
               .HasColumnName("productid");

            builder
                .Property(b => b.Status)
                .HasColumnName("productname");

        }
    }
}
