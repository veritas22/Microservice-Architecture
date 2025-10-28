
using Domain;
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

        public async Task<IEnumerable<Reserve>> GetReserve(int id, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var reserve = await db.Reserve.Where(p => p.Id == id).ToListAsync(cancel);
                return reserve;
            }

        }

        public async Task<int> AddReserve(Reserve reserve, CancellationToken cancel)
        {

            using (var db = await _contextProvider.CreateDbContextAsync(cancel))
            {
                await db.Reserve.AddAsync(reserve, cancel);
                var result = await db.SaveChangesAsync(cancel);
                return reserve.Id;
            }

        }

        public async Task<int> DeleteReserve(Reserve reserve, CancellationToken cancel)
        {

            using (var db = await _contextProvider.CreateDbContextAsync(cancel))
            {
                db.Reserve.Remove(reserve);
                var result = await db.SaveChangesAsync(cancel);
                return result;
            }

        }

    }
}
