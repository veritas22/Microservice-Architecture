using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace EntityCorePostgres.Configuration
{
    class OrferConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("account");

            builder.HasKey(x => x.Id);

            builder
                .Property(b => b.Amount)
               .HasColumnName("amount");

            builder
                .Property(b => b.UserId)
                .HasColumnName("userId");

        }
    }
}
