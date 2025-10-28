using Application.Interfaces.Broker;
using Domen;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQNeuro.DTO;
using System.Diagnostics;
using System.Text;

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

        private async Task CreateChannel()
        {
            _connection =  await _connectionFactory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
        }

        public async Task PublishMessage(int userID, float price, string queueMes, string text, bool status)
        {
            await CreateChannel();
            Debug.WriteLine($"отправили сообщение");

            await _channel.QueueDeclareAsync(queue: queueMes, durable: false, exclusive: false, autoDelete: false,
                arguments: null);
            var mes = new Messange();
            mes.Text = text;
            mes.Status = status;
            mes.Price = price;
            mes.UserID = userID;

            var ser = JsonConvert.SerializeObject(mes);
            var body = Encoding.UTF8.GetBytes(ser);

            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueMes, body: body);
        }

    }
}
