using Application.BusinessLogic;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

using System.Text;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationServise(this IServiceCollection self)
        {

            self.AddScoped(typeof(IAccountLogic), typeof(AccountLogic));
            self.AddScoped(typeof(IBillingLogic), typeof(BillingLogic));

            return self;
        }
    }
}
