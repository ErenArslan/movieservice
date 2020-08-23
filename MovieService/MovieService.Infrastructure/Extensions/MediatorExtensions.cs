using MediatR;
using MovieService.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace MovieService.Infrastructure.Extensions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, Entity entity)
        {
            if (entity.DomainEvents == null || !entity.DomainEvents.Any())
            {
                return;
            }

            var domainEvents = entity.DomainEvents.ToList();

            entity.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);



        }
    }
}
