using Application.Interfaces;
using Domen;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EntityCorePostgres
{
    public class RepositoryOrder<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDbContextFactory<PostgresDBContext> _contextProvider;

        public RepositoryOrder(IDbContextFactory<PostgresDBContext> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        public async Task<int> AddAsync(TEntity entity, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var order = await db.AddAsync(entity, cancel);
                return await db.SaveChangesAsync(cancel);
            }
        }

        public async Task<int> DeleteAsync(TEntity entity, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                 db.Remove<TEntity>(entity);
                 return await db.SaveChangesAsync(cancel);
            }
        }

        public async Task<TEntity> GetAsync(int key, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var order = await db.FindAsync<TEntity>(key);
                return order;
            }

        }
    }
}
