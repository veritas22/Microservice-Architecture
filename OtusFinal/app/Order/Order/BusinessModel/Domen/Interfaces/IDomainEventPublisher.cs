using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen.Interfaces.Event
{
    public interface IDomainEventPublisher
    {
        IReadOnlyCollection<INotification> Events { get; }

        void AddEvent(INotification @event);
        void AddEventRange(IEnumerable<INotification> events);
        Task HandleEvents();
    }
}
