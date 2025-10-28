
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

        public async Task<Account> GetAccount(int userId, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var user = await db.Account.FirstOrDefaultAsync(p => p.UserId == userId);
                return  user ?? new Account();
            }

        }

        public async Task<int> AddAccount(Account account, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var result = await db.Account.AddAsync(account);
                await db.SaveChangesAsync();
                return account.Id;
            }

        }
        public async Task<float> UpdateAccount(int userId, float amount, CancellationToken cancel)
        {

            using (var db = await _contextProvider.CreateDbContextAsync(cancel))
            {
                var account = await db.Account.FirstOrDefaultAsync(p => p.UserId == userId, cancel);
                if (account != null)
                {
                    account.Amount = amount;

                    db.Account.Update(account);
                    await db.SaveChangesAsync(cancel);

                    return account.Amount;
                }
            }
            return -1;

        }

    }
}
