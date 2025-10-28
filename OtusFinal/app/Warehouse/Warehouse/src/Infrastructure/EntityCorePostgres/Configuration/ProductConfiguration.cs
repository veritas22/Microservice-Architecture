using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace EntityCorePostgres.Configuration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");

            builder.HasKey(x => x.Id);

            builder
                .Property(b => b.Price)
               .HasColumnName("price");

            builder
                .Property(b => b.Name)
                .HasColumnName("product_name");

            builder
             .Property(b => b.Description)
             .HasColumnName("description");
            builder
              .Property(b => b.CreationDate)
              .HasColumnName("creation_date")
              .HasColumnType("timestamp(6)"); 
        }
    }
}
