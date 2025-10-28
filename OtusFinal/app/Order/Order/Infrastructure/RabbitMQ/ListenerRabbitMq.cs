using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;

namespace RabbitMQNeuro
{
    class ListenerRabbitMq : BackgroundService
    {
        readonly IConnectionFactory _connectionFactory;

        public ListenerRabbitMq(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             var connection = await _connectionFactory.CreateConnectionAsync();
             var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "ruslantest", durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Debug.WriteLine($"Получено сообщение: {message}");
                //channel.BasicAckAsync(ea.DeliveryTag, false);

                return Task.CompletedTask;
            };
            await channel.BasicConsumeAsync(queue: "ruslantest", autoAck: true, consumer: consumer);
        }
    }
}
