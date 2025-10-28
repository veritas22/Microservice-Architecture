
using Application.Interfaces;
using Entity;
using Entity.Dto;
using Entity.User;
using EntityCorePostgres;
using Microsoft.EntityFrameworkCore;

namespace MongoDateBase
{
    public class AuthRepository: IAuthRepository
    {
        private readonly IDbContextFactory<PostgresDBContext> _contextProvider;

        public AuthRepository(IDbContextFactory<PostgresDBContext> contextProvider)
        {
            _contextProvider = contextProvider;
        }


        public async Task<int> AddAccount(Account account)
        {
            using (var db = _contextProvider.CreateDbContext())
            {
                db.Account.Add(account);
                await db.SaveChangesAsync();
                return account.Id;
            }
        }

        public async Task<Account> GetAccount(string telephone)
        {
            using (var db = _contextProvider.CreateDbContext())
            {
                var result = await db.Account.FirstOrDefaultAsync(p => p.Telephone == telephone);
                return result;
            }
        }

        public async Task UpdateUser(int userId, AccountDto accountDto, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var user = await db.Account.FirstOrDefaultAsync(p => p.Id == userId);
                if (user != null)
                {
                    user.UserName = accountDto.UserName;
                    user.Telephone = accountDto.Telephone;

                    db.Account.Update(user);

                }
            }

        }
    }
    
}
