using Application.BusinessLogic;
using Application.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQNeuro.DTO;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RabbitMQNeuro
{
    class ListenerRabbitMq : BackgroundService
    {
        readonly IConnectionFactory _connectionFactory;
        readonly IMailLogic _mailLogic;
        public ListenerRabbitMq(IConnectionFactory connectionFactory, IMailLogic mailLogic)
        {
            _connectionFactory = connectionFactory;
            _mailLogic = mailLogic;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var queueMail = "mailOrder";
            var connection = await _connectionFactory.CreateConnectionAsync(stoppingToken);
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queueMail, durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async(obj, eventA) => {
                var body = eventA.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var des = JsonConvert.DeserializeObject<Messange>(message);
                var mail = new  Mail();
                mail.Email = "oprw@";
                mail.UserId = des.UserID;
                mail.Name = des.Status.ToString();
                mail.Body = des.Text + des.Status.ToString();

                await _mailLogic.LogMailAsync(mail);
                }; 
            await channel.BasicConsumeAsync(queue: queueMail, autoAck: true, consumer: consumer);


        }

    }
}
