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
        public float Price { get; }
        public int UserId { get; }

        public CourierPlacedEvent(int userId, float price, Guid aggregateId, int eventId)
        {
            Price = price;
            AggregateId = aggregateId;
            EventId = eventId;
        }
    }
}
