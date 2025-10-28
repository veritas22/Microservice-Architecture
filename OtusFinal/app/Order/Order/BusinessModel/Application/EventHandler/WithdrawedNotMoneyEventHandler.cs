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
    public class WithdrawedNotMoneyEventHandler : INotificationHandler<WithdrawedMoneyNotEvent>
    {
        IRepositoryEvent<EventsOrder> _repositoryEvent;
        private readonly ISendMessage _sendMessage;
        private readonly ILogger<WithdrawedNotMoneyEventHandler> _logger;

        public WithdrawedNotMoneyEventHandler(ILogger<WithdrawedNotMoneyEventHandler> logger, IRepositoryEvent<EventsOrder> repositoryEvent, ISendMessage sendMessage) 
        {
            _repositoryEvent = repositoryEvent;
            _sendMessage = sendMessage;
            _logger = logger;
        }

        public async Task Handle(WithdrawedMoneyNotEvent withdrawedMoneyEvent, CancellationToken cancellationToken)
        {
            var eventsOrder = new EventsOrder(withdrawedMoneyEvent.AggregateId, withdrawedMoneyEvent.EventId, typeof(WithdrawedMoneyNotEvent).Name, withdrawedMoneyEvent);
            var result = await _repositoryEvent.AddAsync(eventsOrder, cancellationToken);
            _logger.LogInformation($"Order оформлен не успешно. Событие WithdrawedMoneyNotEvent");

            await _sendMessage.PublishMessage(withdrawedMoneyEvent.UserId, withdrawedMoneyEvent.Price, "mailOrder", $"Order оформлен не успешно. Событие WithdrawedMoneyNotEvent", false);

        }
    }
}
