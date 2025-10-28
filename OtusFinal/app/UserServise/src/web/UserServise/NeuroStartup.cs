using Microsoft.Extensions.Hosting;
using System;
using NeuroStore;
using Interfases;

namespace UserServise
{
    public class NeuroStartup : IHostedService
    {
        private IEFStore _neuroStore;

        public NeuroStartup(IEFStore neuroStore)
        {
            _neuroStore = neuroStore;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {  
            //не придумал эмуляция запроса
            await Task.Delay(100);

            await _neuroStore.Migrate(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //не придумал эмуляция запроса
            await Task.Delay(100);
        }

    }
}
