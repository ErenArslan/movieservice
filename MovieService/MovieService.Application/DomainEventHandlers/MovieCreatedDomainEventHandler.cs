using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MovieService.Domain.Events;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MovieService.Application.DomainEventHandlers
{
    class MovieCreatedDomainEventHandler : INotificationHandler<MovieCreatedDomainEvent>
    {
        private readonly ILogger<MovieCreatedDomainEventHandler> _logger;
        private readonly IDistributedCache _distributedCache;
        public MovieCreatedDomainEventHandler(IDistributedCache distributedCache, ILogger<MovieCreatedDomainEventHandler> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }
        public async Task Handle(MovieCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                await _distributedCache.SetStringAsync(notification.Movie.Id.ToString(),JsonConvert.SerializeObject(notification.Movie));
            }
            catch (Exception ex)
            {

                _logger.LogError("");
            }
        }
    }
}
