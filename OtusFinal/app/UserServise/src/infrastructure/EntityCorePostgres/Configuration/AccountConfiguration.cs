using Entity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace EntityCorePostgres.Configuration
{
    class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("account");

            builder.HasKey(x => x.Id);

            builder
                .Property(b => b.PasswordHash)
               .HasColumnName("password_hash");

            builder
                .Property(b => b.UserName)
                .HasColumnName("user_name");

            builder
                .Property(b => b.Telephone)
                .HasColumnName("telephone");

        }
    }
}
