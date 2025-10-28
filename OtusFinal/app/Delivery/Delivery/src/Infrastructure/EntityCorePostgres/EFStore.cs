
using Application.Interfaces;
using Entity;
using EntityCorePostgres;
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

        public async Task<ReserveCourier> GetReserve(int Id, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var courier = await db.ReserveCourier.FirstOrDefaultAsync(p => p.Id == Id);
                return courier ?? new ReserveCourier();
            }

        }

        public async Task<int> AddReserve(ReserveCourier reserveCourier, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var result = await db.ReserveCourier.AddAsync(reserveCourier);
                await db.SaveChangesAsync();
                return reserveCourier.Id;
            }

        }

        public async Task<int> DeleteReserve(ReserveCourier reserve, CancellationToken cancel)
        {

            using (var db = await _contextProvider.CreateDbContextAsync(cancel))
            {
                db.ReserveCourier.Remove(reserve);
                var result = await db.SaveChangesAsync(cancel);
                return result;
            }

        }

    }
}
