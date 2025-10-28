using Application.BusinessLogic;
using Application.Interfaces;
using Application.Interfaces.Broker;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationServise(this IServiceCollection self)
        {
            self.AddSingleton(typeof(IMailLogic), typeof(MailLogic));

            self.AddSingleton(typeof(ILogicMessage), typeof(RabbitMessage));


            return self;
        }
    }
}
