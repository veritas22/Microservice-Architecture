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
using System.Threading.Tasks;

namespace Application.EventHandler
{
    public class CourierPlacedEventHandler : INotificationHandler<CourierPlacedEvent>
    {
        IRepositoryEvent<EventsOrder> _repositoryEvent;
        private readonly ISendMessage _sendMessage;
        private readonly ILogger<CourierPlacedEventHandler> _logger;


        public CourierPlacedEventHandler(ILogger<CourierPlacedEventHandler> logger, IRepositoryEvent<EventsOrder> repositoryEvent, ISendMessage sendMessage) 
        {
            _repositoryEvent = repositoryEvent;
            _sendMessage = sendMessage;
            _logger = logger;
        }

        public async Task Handle(CourierPlacedEvent orderPlacedEvent, CancellationToken cancellationToken)
        {
            var eventsOrder = new EventsOrder(orderPlacedEvent.AggregateId, orderPlacedEvent.EventId, typeof(CourierPlacedEvent).Name, orderPlacedEvent);
            var result = await _repositoryEvent.AddAsync(eventsOrder, cancellationToken);
            _logger.LogInformation($"Order оформлен успешно. Событие CourierPlacedEvent");

            await _sendMessage.PublishMessage(orderPlacedEvent.UserId, orderPlacedEvent.Price, "mailOrder", $"Order оформлен успешно price {orderPlacedEvent.Price}", true);

        }
    }
}
