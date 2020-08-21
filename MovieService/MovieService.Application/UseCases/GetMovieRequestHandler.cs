using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieService.Application.Dtos.Requests;
using MovieService.Application.Dtos.Responses;
using MovieService.Domain.AggregatesModels.Movie;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MovieService.Application.UseCases
{
    public class GetMovieRequestHandler : IRequestHandler<GetMovieRequest, BaseResponse<MovieDto>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<GetMovieRequestHandler> _logger;

        public GetMovieRequestHandler(IMovieRepository movieRepository, ILogger<GetMovieRequestHandler> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }
        public async Task<BaseResponse<MovieDto>> Handle(GetMovieRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<MovieDto> response = new BaseResponse<MovieDto>();

            try
            {
                var movie = await _movieRepository.Get(request.Id);
                response.Data = movie.Adapt<MovieDto>();
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
