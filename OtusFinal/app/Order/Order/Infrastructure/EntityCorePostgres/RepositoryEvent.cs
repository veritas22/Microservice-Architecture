using Application.Interfaces;
using Domen;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityCorePostgres
{
    internal class RepositoryEvent<TEntity> : IRepositoryEvent<TEntity> where TEntity : class
    {
        private readonly IDbContextFactory<PostgresDBContext> _contextProvider;

        public RepositoryEvent(IDbContextFactory<PostgresDBContext> contextProvider)
        {
            _contextProvider = contextProvider;
        }

        

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancel)
        {

            using (var db = _contextProvider.CreateDbContext())
            {
                var order = await db.AddAsync(entity, cancel);
                await db.SaveChangesAsync(cancel);

                return entity;
            }

        }
    }
}
