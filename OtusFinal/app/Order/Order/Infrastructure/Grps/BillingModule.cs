using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace Grps
{
    public static class BillingModule
    {
        public static IServiceCollection AddBillingService(this IServiceCollection self)
        {
            self.AddSingleton<IBillling, BillingServise>();
            self.AddSingleton<IWarehouse, WarehouseServise>();
            self.AddSingleton<IDelivery, DeliveryServise>();
            return self;
        }
    }
}
