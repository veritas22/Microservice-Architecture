using Entity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityCorePostgres.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(x => x.Id);

            builder
                .Property(b => b.Username)
               .HasColumnName("user_name");

            builder
                .Property(b => b.FirstName)
                .HasColumnName("first_name");

            builder
                .Property(b => b.LastName)
                .HasColumnName("last_name");

            builder
             .Property(b => b.Email)
             .HasColumnName("email");


            builder
             .Property(b => b.Phone)
             .HasColumnName("phone");


        }
    }
}
