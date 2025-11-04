using Domen.ValueObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Domen.Aggregate.Events
{
    public class OrderNotPlacedEvent : INotification
    {
        public int EventId { get; }
        public Guid AggregateId { get; }
        public int UserId { get; }
        public int ReserveId { get; }
        public Price Price { get; }
        public OrderNotPlacedEvent(int userId, int reserveId, Price price, Guid aggregateId, int eventId)
        {
            UserId = userId;
            ReserveId = reserveId;
            Price = price;
            AggregateId = aggregateId;
            EventId = eventId;
        }

    }
}
