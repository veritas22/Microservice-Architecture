
using Application.Interfaces;
using Domain;
using EntityCorePostgres;
using Interfases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Npgsql.Internal;

namespace NeuroStore.Repository
{
    public class ProductStore : IProductStore
    {
        private readonly IDbContextFactory<PostgresDBContext> _contextProvider;

        public ProductStore(IDbContextFactory<PostgresDBContext> contextProvider)
        {
            _contextProvider = contextProvider;
        }



        public async Task<IEnumerable<Product>> GetProduct(int id, CancellationToken cancel)
        {

            using (var db = await _contextProvider.CreateDbContextAsync(cancel))
            {
                var user = await db.Product.Where(p => p.Id == id).ToListAsync(cancel);
                return  user;
            }

        }

        public async Task<int> AddProduct(Product product, CancellationToken cancel)
        {

            using (var db = await _contextProvider.CreateDbContextAsync(cancel))
            {
                var result = await db.Product.AddAsync(product, cancel);
                await db.SaveChangesAsync(cancel);
                return product.Id;
            }

        }
        

    }
}
