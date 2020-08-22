using DotNetCore.CAP;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieService.Application.Dtos.Requests;
using MovieService.Application.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace MovieService.Application.IntegrationEvents.Handlers
{
    public class MovieFetchedIntegrationEventHandler : ICapSubscribe
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MovieFetchedIntegrationEventHandler> _logger;
        public MovieFetchedIntegrationEventHandler(IMediator mediator, ILogger<MovieFetchedIntegrationEventHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [CapSubscribe(nameof(MovieFetchedIntegrationEvent))]
        public async Task Handle(MovieFetchedIntegrationEvent @event)
        {
            try
            {
                var addMovieRequest = @event.Adapt<AddMovieRequest>();
                await _mediator.Send(addMovieRequest);
            }
            catch (Exception)
            {

                _logger.LogError("");
            }
        }
    }
}
