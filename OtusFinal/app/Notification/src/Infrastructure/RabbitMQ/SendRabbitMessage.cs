using Application.Interfaces.Broker;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RabbitMQNeuro
{
    public class SendRabbitMessage: ISendMessage
    {
        readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IChannel _channel;

        public SendRabbitMessage(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        public async Task PublishMessage(string text)
        {
            _connection = await _connectionFactory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            Debug.WriteLine($"отправили сообщение");

            await _channel.QueueDeclareAsync(queue: "ruslantest", durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(text);

            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: "ruslantest", body: body);
        }
    }
}
