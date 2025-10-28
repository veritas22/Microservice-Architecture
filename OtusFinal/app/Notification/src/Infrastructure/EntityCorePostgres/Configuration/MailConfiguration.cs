using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace EntityCorePostgres.Configuration
{
    class MailConfiguration : IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder.ToTable("mail");

            builder.HasKey(x => x.Id);

            builder
                .Property(b => b.Body)
               .HasColumnName("body");

            builder
                .Property(b => b.Name)
                .HasColumnName("name");

            builder
                .Property(b => b.Email)
                .HasColumnName("email");

            builder
             .Property(b => b.UserId)
             .HasColumnName("user_id");

        }
    }
}
