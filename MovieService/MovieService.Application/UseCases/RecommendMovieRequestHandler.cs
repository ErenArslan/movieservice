using DotNetCore.CAP;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieService.Application.Dtos.Requests;
using MovieService.Application.Dtos.Responses;
using MovieService.Application.IntegrationEvents.Events;
using MovieService.Domain.AggregatesModels.Movie;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MovieService.Application.UseCases
{
    public  class RecommendMovieRequestHandler : IRequestHandler<RecommendMovieRequest, BaseResponse<bool>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<RecommendMovieRequestHandler> _logger;
        private readonly ICapPublisher _eventBus;
        public RecommendMovieRequestHandler(IMovieRepository movieRepository,ICapPublisher eventBus, ILogger<RecommendMovieRequestHandler> logger)
        {
            _movieRepository = movieRepository;
            _eventBus = eventBus;
            _logger = logger;
        }
        public async Task<BaseResponse<bool>> Handle(RecommendMovieRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                var movie = await _movieRepository.Get(request.MovieId);
                var movieRecommendedIntegrationEvent = 
                    new MovieRecommendedIntegrationEvent(movie.Id,movie.Title,movie.Overview,movie.ReleaseDate,request.Email);

                await _eventBus.PublishAsync<MovieRecommendedIntegrationEvent>(typeof(MovieRecommendedIntegrationEvent).Name,movieRecommendedIntegrationEvent);
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                _logger.LogError("");

            }
            return response;
        }
    }
}
