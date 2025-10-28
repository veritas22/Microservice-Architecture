using Application.Interfaces;
using Application.Interfaces.Broker;
using Domen;
using Domen.Aggregate.Events;
using Domen.Interfaces.Event;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.BusinessLogic
{
    public class OrderLogic: IOrderLogic
    {
        private readonly ISendMessage _sendMessage;
        private readonly IBillling _billling;
        private readonly ILogger<OrderLogic> _logger;
        private readonly IMediator _mediator;
        private readonly  IDomainEventPublisher _publisher;

        public OrderLogic(ILogger<OrderLogic> logger, ISendMessage sendMessage, IBillling billling, IMediator mediator, IDomainEventPublisher publisher)
        {
            _sendMessage = sendMessage;
            _billling = billling;
            _logger = logger;
            _mediator = mediator;
            _publisher = publisher;
        }


        
        /// <summary>
        /// Сага
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="price"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        public async Task<float> PlaceOrderAsync(int userId, float price, CancellationToken cancel)
        {
            try
            {

                var result = await _billling.WithdrawMoneyAsync(userId, price);
                Guid aggregateId = Guid.NewGuid();

                if (result != -1)
                {
                    await _mediator.Publish(new WithdrawedMoneyEvent( userId,  price, aggregateId, 0));

                }
                else
                {
                    await _mediator.Publish(new WithdrawedMoneyNotEvent(userId, price, aggregateId,0));
                }

                return result;


            }
            catch (Exception ex ) 
            {
                await _sendMessage.PublishMessage(0, 0, "mailOrder", $"Идет не по плану {ex.Message}", false);
                return 0;

            }
        }
    }
}
