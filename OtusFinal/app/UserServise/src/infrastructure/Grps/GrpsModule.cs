using Application.Interfaces;
using Grps.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Grps
{
    public static class GrpsModule
    {
        public static IServiceCollection AddGrpsService(this IServiceCollection self)
        {
            self.AddSingleton<IAccountServise, AccountServise>();

            return self;
        }
    }


}
