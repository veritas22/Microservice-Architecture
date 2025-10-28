using Application.Interfaces;
using Entity.Dto;
using Entity.User;
using EntityCorePostgres;
using Interfases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Npgsql.Internal;

namespace NeuroStore.Repository
{
    public class EFStore : IEFStore
    {
        private readonly IDbContextFactory<PostgresDBContext> _contextProvider;

        public EFStore(IDbContextFactory<PostgresDBContext> contextProvider)
        {
            _contextProvider = contextProvider;
        }


        public async Task Migrate(CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                await db.Database.MigrateAsync(cancel);
            }

        }

        public async Task<User> GetUser(int userId, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var user = await db.User.FirstOrDefaultAsync(p => p.Id == userId);
                user = user ?? new User();
                return user;
            }

        }

        public async Task AddUser(UserDto userDto , CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {

                var user = new User();
                user.Username = userDto.Username;
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.Email = userDto.Email;
                user.Phone =userDto.Phone;
                var result = await db.User.AddAsync(user);
                await db.SaveChangesAsync();
            }

        }

        public async Task DeleteUser(int userId , CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var user = await db.User.FirstOrDefaultAsync(p => p.Id == userId);
                if (user != null)
                {
                    db.User.Remove(user);
                    await db.SaveChangesAsync();

                }
            }

        }

        public async Task UpdateUser(int userId, UserDto userDto, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var user = await db.User.FirstOrDefaultAsync(p => p.Id == userId);
                if (user != null)
                {
                    user.FirstName = userDto.FirstName;
                    user.LastName = userDto.LastName;
                    user.Email = userDto.Email;
                    user.Phone = userDto.Phone;
                        
                    db.User.Update(user);

                }
            }

        }

    }
}
