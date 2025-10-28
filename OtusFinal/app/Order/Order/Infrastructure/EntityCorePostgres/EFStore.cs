using Application.Interfaces;
using Domen;
using EntityCorePostgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Npgsql.Internal;

namespace EntityCorePostgres
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

        public async Task<Order> GetOrder(int userId, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var order = await db.Order.FirstOrDefaultAsync(p => p.UserID == userId);
                order = order ?? new Order();
                return order;
            }

        }

        public async Task<int> AddOrder(Order order , CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {

                var result = await db.Order.AddAsync(order);
                return await db.SaveChangesAsync();
            }

        }

    }
}
