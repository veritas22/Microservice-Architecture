using Application.BusinessLogic;
using Application.Interfaces;
using Application.Interfaces.Broker;
using Domen.Interfaces.Event;
using Domen.Root;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationServise(this IServiceCollection self, IConfiguration config)
        {
            self.AddScoped(typeof(IDomainEventPublisher), typeof(DomainEventPublisher));

            self.AddScoped(typeof(ILogicMessage), typeof(RabbitMessage));
            self.AddScoped(typeof(IOrderLogic), typeof(OrderLogic));
            self.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return self;
        }
    }
}
