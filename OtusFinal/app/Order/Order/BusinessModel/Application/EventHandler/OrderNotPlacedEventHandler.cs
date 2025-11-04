using Application.BusinessLogic;
using Application.Interfaces;
using Application.Interfaces.Broker;
using Domen;
using Domen.Aggregate;
using Domen.Aggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Application.EventHandler
{
    public class OrderNotPlacedEventHandler : INotificationHandler<OrderNotPlacedEvent>
    {
        IRepositoryEvent<EventsOrder> _repositoryEvent;
        private readonly ISendMessage _sendMessage;
        private readonly IBillling _billling;
        private readonly IWarehouse _warehouse;
        private readonly ILogger<ResrveWarehouseEventHandler> _logger;

        public OrderNotPlacedEventHandler(ILogger<ResrveWarehouseEventHandler> logger, IRepositoryEvent<EventsOrder> repositoryEvent, IBillling billling, IWarehouse warehouse, ISendMessage sendMessage)
        {
            _repositoryEvent = repositoryEvent;
            _logger = logger;
            _billling = billling;
            _warehouse = warehouse;
            _sendMessage = sendMessage;
        }

        public async Task Handle(OrderNotPlacedEvent resrvedEvent, CancellationToken cancellationToken)
        {
            var eventsOrder = new EventsOrder(resrvedEvent.AggregateId, resrvedEvent.EventId, typeof(OrderNotPlacedEvent).Name, resrvedEvent);
            var result = await _repositoryEvent.AddAsync(eventsOrder, cancellationToken);
            _logger.LogInformation($"Событие OrderNotPlacedEvent");

            var statusWar = await _warehouse.DeleteReserveAsync(resrvedEvent.ReserveId);
            //_logger.LogInformation($"_warehouse запись удалена resrveId {resrveId}");

            var depositBil = await _billling.DepositMoneyAsync(resrvedEvent.UserId, resrvedEvent.Price.Value);
            //_logger.LogInformation($"_billling откат userId  {userId} price {price}");

            await _sendMessage.PublishMessage(resrvedEvent.UserId, resrvedEvent.Price.Value, "mailOrder", $"Order оформлен не успешно. событие OrderNotPlacedEvent", false);
            //_logger.LogInformation($"_sendMessage сообщение отправлено");
        }
    }
}
