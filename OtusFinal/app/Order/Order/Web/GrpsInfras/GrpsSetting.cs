using static Grps.BillingServiseGrps;
using static Grps.DeliveryServiseGrps;
using static Grps.WarehouseServiseGrps;

namespace Order.Grps
{
    public static class GrpsSetting
    {
        public static IServiceCollection AddGrpsOrder(this IServiceCollection self, IConfiguration config)
        {
            self.AddSingleton<GrpcLoggingInterceptor>();

            self.AddGrpcClient<BillingServiseGrpsClient>(o =>
            {
                o.Address = new Uri($"{config.GetSection("GrpsBillingConnection").Value}");
            }).AddInterceptor<GrpcLoggingInterceptor>();
            self.AddGrpcClient<WarehouseServiseGrpsClient>(o =>
            {
                o.Address = new Uri($"{config.GetSection("GrpsWarehouseConnection").Value}");
            }).AddInterceptor<GrpcLoggingInterceptor>();
            self.AddGrpcClient<DeliveryServiseGrpsClient>(o =>
            {
                o.Address = new Uri($"{config.GetSection("GrpsDeliveryConnection").Value}");
            }).AddInterceptor<GrpcLoggingInterceptor>();
            return self;
        }
    }
}
