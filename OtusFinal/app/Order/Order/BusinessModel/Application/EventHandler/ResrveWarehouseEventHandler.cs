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
    public class ResrveWarehouseEventHandler : INotificationHandler<ResrvedWarehouseEvent>
    {
        IRepositoryEvent<EventsOrder> _repositoryEvent;
        private readonly ISendMessage _sendMessage;
        private readonly IBillling _billling;
        private readonly IWarehouse _warehouse;
        private readonly IDelivery _delivery;
        private readonly IMediator _mediator;
        private readonly IRepository<Order> _repositoryOrder;
        private readonly ILogger<ResrveWarehouseEventHandler> _logger;

        public ResrveWarehouseEventHandler(ILogger<ResrveWarehouseEventHandler> logger, IRepositoryEvent<EventsOrder> repositoryEvent,  IDelivery delivery, IMediator mediator, IRepository<Order> repositoryOrder)
        {
            _delivery = delivery;
            _repositoryEvent = repositoryEvent;
            _mediator = mediator;
            _repositoryOrder = repositoryOrder;
            _logger = logger;
        }

        public async Task Handle(ResrvedWarehouseEvent resrvedEvent, CancellationToken cancellationToken)
        {
            var eventsOrder = new EventsOrder(resrvedEvent.AggregateId, resrvedEvent.EventId, typeof(ResrvedWarehouseEvent).Name, resrvedEvent);
            var result = await _repositoryEvent.AddAsync(eventsOrder, cancellationToken);
            _logger.LogInformation($"Событие ResrvedWarehouseEvent");

            var order = new Order() { Price = resrvedEvent.Price, UserID = resrvedEvent.UserId, Status = "Успех" };
            var id = await _repositoryOrder.AddAsync(order, cancellationToken);
            if (id > 0)
            {

                await _mediator.Publish(new OrderPlacedEvent(order.Id, resrvedEvent.ReserveId, resrvedEvent.UserId, resrvedEvent.Price, resrvedEvent.AggregateId, resrvedEvent.EventId+1));
            }
            else
            {
                await _mediator.Publish(new OrderNotPlacedEvent( resrvedEvent.UserId, resrvedEvent.ReserveId, resrvedEvent.Price, resrvedEvent.AggregateId, resrvedEvent.EventId + 1));
            }

        }
    }
}
