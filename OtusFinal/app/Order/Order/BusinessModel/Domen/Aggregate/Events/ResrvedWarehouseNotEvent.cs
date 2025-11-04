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
    public class ResrvedWarehouseNotEvent : INotification
    {
        public int EventId { get; }
        public Guid AggregateId { get; }
        public int UserId { get; }
        public Price Price { get; }

        public ResrvedWarehouseNotEvent( int userId, Price price, Guid aggregateId, int eventId)
        {
            UserId = userId;
            Price = price;
            AggregateId = aggregateId;
            EventId = eventId;
        }

    }
}
