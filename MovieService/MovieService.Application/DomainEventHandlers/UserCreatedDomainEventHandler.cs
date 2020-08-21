using MediatR;
using MovieService.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MovieService.Application.DomainEventHandlers
{
    class UserCreatedDomainEventHandler : INotificationHandler<UserCreatedDomainEvent>
    {
        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            // We can publish via Message Bus (RabbitMQ,Kafka) for other services. We can use event sourcing.

            await Task.CompletedTask;
        }
    }
}
