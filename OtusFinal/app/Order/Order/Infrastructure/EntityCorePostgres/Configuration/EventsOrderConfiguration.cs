using Domen;
using Domen.Aggregate;
using Domen.Aggregate.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace EntityCorePostgres.Configuration
{
    public class EventsOrderConfiguration : IEntityTypeConfiguration<EventsOrder>
    {
        public void Configure(EntityTypeBuilder<EventsOrder> builder)
        {
            builder.ToTable("order_event");

            builder.HasKey(x => x.Id);

            builder
                .Property(b => b.Data)
                .HasColumnName("data");

            builder
                .Property(b => b.EventId)
                .HasColumnName("event_id");

            builder
                .Property(b => b.EventType)
                .HasColumnName("event_type");

            builder
                .Property(b => b.AggregateType)
                .HasColumnName("aggregate_type");

            builder
                .Property(b => b.AggregateId)
                .HasColumnName("aggregate_id");

        }
    }
}
