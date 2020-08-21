using DotNetCore.CAP;
using MovieService.Application.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace MovieService.Application.IntegrationEvents.Handlers
{
   public class MovieRecommendedIntegrationEventHandler : ICapSubscribe
    {


        [CapSubscribe(nameof(MovieRecommendedIntegrationEvent))]
        public async Task Handle(MovieRecommendedIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
