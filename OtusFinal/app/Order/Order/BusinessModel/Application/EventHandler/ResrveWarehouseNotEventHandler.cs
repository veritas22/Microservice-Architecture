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
    public class ResrveWarehouseNotEventHandler : INotificationHandler<ResrvedWarehouseNotEvent>
    {
        IRepositoryEvent<EventsOrder> _repositoryEvent;
        private readonly ISendMessage _sendMessage;
        private readonly IBillling _billling;
        private readonly IWarehouse _warehouse;
        private readonly IDelivery _delivery;
        private readonly ILogger<ResrveWarehouseNotEventHandler> _logger;

        public ResrveWarehouseNotEventHandler(ILogger<ResrveWarehouseNotEventHandler> logger, IRepositoryEvent<EventsOrder> repositoryEvent, ISendMessage sendMessage,  IBillling billling, IWarehouse warehouse, IDelivery delivery ) 
        {
            _repositoryEvent = repositoryEvent;
            _sendMessage = sendMessage;
            _billling = billling;
            _warehouse = warehouse;
            _logger = logger;
        }

        public async Task Handle(ResrvedWarehouseNotEvent resrvedNotEvent, CancellationToken cancellationToken)
        {
            var eventsOrder = new EventsOrder(resrvedNotEvent.AggregateId, resrvedNotEvent.EventId, typeof(ResrvedWarehouseNotEvent).Name, resrvedNotEvent);
            var result = await _repositoryEvent.AddAsync(eventsOrder, cancellationToken);
            _logger.LogInformation($"Order оформлен не успешно. Событие ResrvedWarehouseNotEvent");
            var deposit = await _billling.DepositMoneyAsync(resrvedNotEvent.UserId, resrvedNotEvent.Price);


        }
    }
}
