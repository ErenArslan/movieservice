using DotNetCore.CAP;
using MovieService.Application.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace MovieService.Application.IntegrationEvents.Handlers
{
    public class MovieFetchedIntegrationEventHandler : ICapSubscribe
    {


        [CapSubscribe(nameof(MovieFetchedIntegrationEvent))]
        public async Task Handle(MovieFetchedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
