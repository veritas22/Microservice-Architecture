using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen.Aggregate
{
    public class EventsOrder
    {
        public int Id { get; }
        public Guid AggregateId { get; }
        public string AggregateType { get; }
        public string EventType { get; }
        public int EventId { get; }
        public string Data { get; }
        public EventsOrder()
        { }

        public EventsOrder(Guid aggregateId, int eventId, string eventType, INotification notification) 
        {
            AggregateId = aggregateId;
            AggregateType = "Order";
            EventId = eventId;
            EventType = eventType;
            Data = JsonConvert.SerializeObject(notification);
        }
    }
}
