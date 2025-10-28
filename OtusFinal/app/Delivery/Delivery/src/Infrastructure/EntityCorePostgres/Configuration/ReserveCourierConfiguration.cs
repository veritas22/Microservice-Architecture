using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace EntityCorePostgres.Configuration
{
    class ReserveCourierConfiguration : IEntityTypeConfiguration<ReserveCourier>
    {
        public void Configure(EntityTypeBuilder<ReserveCourier> builder)
        {
            builder.ToTable("reservecourier");

            builder.HasKey(x => x.Id);

            builder
                .Property(b => b.TimeSlot)
               .HasColumnName("time_slot")
               .HasColumnType("timestamp(6)");

            builder
                .Property(b => b.UserId)
                .HasColumnName("user_id");

            builder
              .Property(b => b.OrderId)
              .HasColumnName("order_id");

            builder
              .Property(b => b.CourierId)
              .HasColumnName("courier_id");

        }
    }
}
