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
    public class CourierNotPlacedEvent : INotification
    {
        public int EventId { get; }
        public Guid AggregateId { get; }
        public int ReserveId { get;  }
        public int UserId { get; }
        public Price Price { get; }
        public int OrderId { get; }


        public CourierNotPlacedEvent(int orderId, int reserveId, int userId, Price price,Guid aggregateId, int eventId)
        {
            ReserveId = reserveId;
            UserId = userId;
            Price = price;
            AggregateId = aggregateId;
            EventId = eventId;
            OrderId = orderId;
        }

    }
}
