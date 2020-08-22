﻿using MediatR;
using MovieService.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace MovieService.Infrastructure.Extensions
{
    static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, Entity entity)
        {
            if (entity.DomainEvents==null || !entity.DomainEvents.Any())
            {
                return;
            }

            foreach (var domainEvent in entity.DomainEvents)
                await mediator.Publish(domainEvent);


            entity.ClearDomainEvents();
        }
    }
}
