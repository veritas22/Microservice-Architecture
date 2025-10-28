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
    public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        IRepositoryEvent<EventsOrder> _repositoryEvent;
        private readonly ISendMessage _sendMessage;
        private readonly IDelivery _delivery;
        private readonly IMediator _mediator;
        private readonly ILogger<ResrveWarehouseEventHandler> _logger;

        public OrderPlacedEventHandler(ILogger<ResrveWarehouseEventHandler> logger, IRepositoryEvent<EventsOrder> repositoryEvent,  IDelivery delivery, IMediator mediator)
        {
            _delivery = delivery;
            _repositoryEvent = repositoryEvent;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(OrderPlacedEvent orderPlacedEvent, CancellationToken cancellationToken)
        {
            var eventsOrder = new EventsOrder(orderPlacedEvent.AggregateId, orderPlacedEvent.EventId, typeof(OrderPlacedEvent).Name, orderPlacedEvent);
            var result = await _repositoryEvent.AddAsync(eventsOrder, cancellationToken);
            _logger.LogInformation($"Событие OrderPlacedEvent");

            var courierId = await _delivery.AddReserveAsync(orderPlacedEvent.OrderId, orderPlacedEvent.UserId, cancellationToken);
            if (courierId > 0)
            {

                await _mediator.Publish(new CourierPlacedEvent(orderPlacedEvent.UserId, orderPlacedEvent.Price, orderPlacedEvent.AggregateId, orderPlacedEvent.EventId + 1));
            }
            else
            {
                await _mediator.Publish(new CourierNotPlacedEvent(orderPlacedEvent.OrderId, orderPlacedEvent.ReserveId, orderPlacedEvent.UserId, orderPlacedEvent.Price,  orderPlacedEvent.AggregateId, orderPlacedEvent.EventId + 1));
            }

        }
    }
}
