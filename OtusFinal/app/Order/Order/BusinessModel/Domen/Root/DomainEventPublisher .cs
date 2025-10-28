using Domen.Interfaces.Event;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen.Root
{
    public class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly IMediator _mediator;
        private readonly List<INotification> _events = [];

        public DomainEventPublisher(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Коллекция событий, возникших за время выполнения операции
        public IReadOnlyCollection<INotification> Events => _events.AsReadOnly();

        // Добавление нового события
        public void AddEvent(INotification @event) => _events.Add(@event);

        // Добавление набора событий
        public void AddEventRange(IEnumerable<INotification> events) => _events.AddRange(events);

        // Обработка всех событий через публикацию в MediatR.
        // MediatR вызывает все существующие обработчики 
        // для конкретного типа события
        public async Task HandleEvents()
        {
            foreach (var @event in _events)
            {
                await _mediator.Publish(@event);
            }
        }
    }
}
