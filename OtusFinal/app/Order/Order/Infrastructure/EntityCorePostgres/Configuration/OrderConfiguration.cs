using Domen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace EntityCorePostgres.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");

            builder.HasKey(x => x.Id);

            builder
                .Property(b => b.UserID)
               .HasColumnName("user_id");

            builder
                .Property(b => b.Price)
                .HasColumnName("price");

            builder
                .Property(b => b.Status)
                .HasColumnName("status");

        }
    }
}
