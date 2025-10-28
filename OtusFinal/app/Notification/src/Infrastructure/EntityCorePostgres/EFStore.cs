
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

        public async Task<IEnumerable<Mail>> GetMail(int userId, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var user = await db.Mail.Where(p => p.UserId == userId).ToListAsync(cancel);
                return  user;
            }

        }

        public async Task AddMail(Mail mail , CancellationToken cancel)
        {

            using (var db = await _contextProvider.CreateDbContextAsync(cancel))
            {
                var result = await db.Mail.AddAsync(mail, cancel);
                await db.SaveChangesAsync(cancel);
            }

        }
        

    }
}
