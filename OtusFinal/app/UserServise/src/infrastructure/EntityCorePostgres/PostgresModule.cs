using Application.Interfaces;
using Interfases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDateBase;
using NeuroStore.Repository;

namespace EntityCorePostgres
{
    public static class PostgresModule
    {

        /// <summary>
        /// Надо следить грамотно за миграцией
        /// PM> add-migration create -context PostgresDBContext
        /// PM>  Update-Database 20250221205433_create
        /// 
        /// обновляем до 0
        /// PM>  Update-Database 0
        /// PM>  Remove-Migration -context PostgresDBContext
        /// PM> add-migration create -context PostgresDBContext
        /// 
        /// применяем новые изменения
        /// PM> add-migration -context PostgresDBContext
        /// 
        /// откатываем назад до нужной версии
        /// Update-Database 20250224133140_v9
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddEFPostgresrStores(this IServiceCollection self, string connectionString)
        {

            self.AddDbContextFactory<PostgresDBContext>(opt =>
            {
                opt.UseNpgsql(connectionString);
            });

            self.AddSingleton(typeof(IEFStore), typeof(EFStore));
            self.AddScoped(typeof(IAuthRepository), typeof(AuthRepository));

            return self;
        }

    }
}
