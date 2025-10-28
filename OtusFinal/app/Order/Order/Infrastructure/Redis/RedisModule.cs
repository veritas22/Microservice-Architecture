using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis.Config;

namespace Redis
{
    public static class RedisModule
    {
        public static IServiceCollection AddRedisService(this IServiceCollection self, IConfiguration config)
        {
            var settings = config.GetSection("Redis").Value;

            self.AddStackExchangeRedisCache(options => {
                options.Configuration = settings;
                options.InstanceName = "Order";
            });
            self.AddScoped<ICache, Redis>();

            return self;
        }

    }
}
