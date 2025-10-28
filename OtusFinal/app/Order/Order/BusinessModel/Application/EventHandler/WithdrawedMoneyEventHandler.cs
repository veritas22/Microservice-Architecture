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
    public class WithdrawedMoneyEventHandler : INotificationHandler<WithdrawedMoneyEvent>
    {
        IRepositoryEvent<EventsOrder> _repositoryEvent;
        private readonly ISendMessage _sendMessage;
        private readonly IBillling _billling;
        private readonly IWarehouse _warehouse;
        private readonly IDelivery _delivery;
        private readonly IMediator _mediator;
        private readonly ILogger<WithdrawedMoneyEventHandler> _logger;

        public WithdrawedMoneyEventHandler(ILogger<WithdrawedMoneyEventHandler> logger, IRepositoryEvent<EventsOrder> repositoryEvent, ISendMessage sendMessage,  IBillling billling, IWarehouse warehouse, IDelivery delivery, IMediator mediator) 
        {
            _repositoryEvent = repositoryEvent;
            _sendMessage = sendMessage;
            _billling = billling;
            _warehouse = warehouse;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(WithdrawedMoneyEvent withdrawedMoneyEvent, CancellationToken cancellationToken)
        {
            var eventsOrder = new EventsOrder(withdrawedMoneyEvent.AggregateId, withdrawedMoneyEvent.EventId, typeof(WithdrawedMoneyEvent).Name, withdrawedMoneyEvent);
            var result = await _repositoryEvent.AddAsync(eventsOrder, cancellationToken);
            _logger.LogInformation($"Событие WithdrawedMoneyEvent");

            var productId = 1;
            var resrveId = await _warehouse.AddReserveAsync(productId);

            if (resrveId > 0)
            {
                await _mediator.Publish(new ResrvedWarehouseEvent( withdrawedMoneyEvent.UserId, resrveId, withdrawedMoneyEvent.Price, withdrawedMoneyEvent.AggregateId, withdrawedMoneyEvent.EventId + 1));
            }
            else
            {
                await _mediator.Publish(new ResrvedWarehouseNotEvent( withdrawedMoneyEvent.UserId, withdrawedMoneyEvent.Price, withdrawedMoneyEvent.AggregateId, withdrawedMoneyEvent.EventId + 1));

            }

        }
    }
}
