using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Application.Interfaces.Broker;
namespace RabbitMQNeuro
{
    public static class RabbitMQModule
    {

        public static IServiceCollection AddRabbitMQservice(this IServiceCollection self)
        {

            self.AddSingleton<RabbitMQ.Client.IConnectionFactory>
                (p => 
                    {
                        var options = p.GetRequiredService<IOptions<RabbitMqConfig>>().Value;

                        var result = new ConnectionFactory
                        {
                            HostName = options.HostName,
                            Port = options.Port,
                            UserName = options.UserName,
                            Password = options.Password,
                        };
                        return result;
                    });
            self.AddSingleton<ISendMessage, SendRabbitMessage>();
            self.AddHostedService<ListenerRabbitMq>();

            return self;
        }
    }
}
