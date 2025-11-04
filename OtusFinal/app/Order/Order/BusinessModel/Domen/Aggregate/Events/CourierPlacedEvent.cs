using Domen.ValueObject;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen.Aggregate.Events
{
    public class CourierPlacedEvent : INotification
    {
        public int EventId { get; }
        public Guid AggregateId { get; }
        public Price Price { get; }
        public int UserId { get; }

        public CourierPlacedEvent(int userId, Price price, Guid aggregateId, int eventId)
        {
            Price = price;
            AggregateId = aggregateId;
            EventId = eventId;
        }
    }
}
