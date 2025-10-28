using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;


namespace Redis
{
    class Redis: ICache
    {

        private readonly IDistributedCache _cache;
        public Redis(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
        }
        public async Task<string> SetRedis()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;


            //тест
            await _cache.SetStringAsync("ggg", "проверка системы", cancel);

            return "не придумал";
        }

        public async Task<string> GetRedis()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken cancel = cancelTokenSource.Token;


            //тест
            var paiment = await _cache.GetStringAsync("ggg", cancel);

            return paiment;
        }
    }
}
