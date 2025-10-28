using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Domen.Aggregate.Events
{
    public class ResrvedWarehouseEvent : INotification
    {
        public int EventId { get; }
        public Guid AggregateId { get; }
        public int UserId { get; }
        public int ReserveId { get; }
        public float Price { get; }
        public ResrvedWarehouseEvent(int userId, int reserveId, float price, Guid aggregateId, int eventId)
        {
            UserId = userId;
            ReserveId = reserveId;
            Price = price;
            AggregateId = aggregateId;
            EventId = eventId;
        }

    }
}
