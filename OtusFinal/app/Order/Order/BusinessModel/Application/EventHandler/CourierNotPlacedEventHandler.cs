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
    public class CourierNotPlacedEventHandler : INotificationHandler<CourierNotPlacedEvent>
    {
        IRepositoryEvent<EventsOrder> _repositoryEvent;
        private readonly ISendMessage _sendMessage;
        private readonly IBillling _billling;
        private readonly IWarehouse _warehouse;
        private readonly ILogger<CourierNotPlacedEventHandler> _logger;
        private readonly IRepository<Order> _repositoryOrder;

        public CourierNotPlacedEventHandler(ILogger<CourierNotPlacedEventHandler> logger, IRepositoryEvent<EventsOrder> repositoryEvent, ISendMessage sendMessage, IRepository<Order> repositoryOrder, IBillling billling, IWarehouse warehouse) 
        {
            _repositoryEvent = repositoryEvent;
            _sendMessage = sendMessage;
            _billling = billling;
            _warehouse = warehouse;
            _logger = logger;
            _repositoryOrder = repositoryOrder;
        }

        public async Task Handle(CourierNotPlacedEvent orderNotPlacedEvent, CancellationToken cancellationToken)
        {
            var eventsOrder = new EventsOrder(orderNotPlacedEvent.AggregateId, orderNotPlacedEvent.EventId, typeof(CourierNotPlacedEvent).Name, orderNotPlacedEvent);
            var result = await _repositoryEvent.AddAsync(eventsOrder, cancellationToken);
            _logger.LogInformation($"Не успешно. Событие OrderNotPlacedEvent");


            var statusWar = await _warehouse.DeleteReserveAsync(orderNotPlacedEvent.ReserveId);
            //_logger.LogInformation($"_warehouse запись удалена resrveId {resrveId}");

            var depositBil = await _billling.DepositMoneyAsync(orderNotPlacedEvent.UserId, orderNotPlacedEvent.Price.Value);
            //_logger.LogInformation($"_billling откат userId  {userId} price {price}");

            await _sendMessage.PublishMessage(orderNotPlacedEvent.UserId, orderNotPlacedEvent.Price.Value, "mailOrder", $"Order оформлен не успешно. событие OrderNotPlacedEvent", false);
            //_logger.LogInformation($"_sendMessage сообщение отправлено");

        }
    }
}
