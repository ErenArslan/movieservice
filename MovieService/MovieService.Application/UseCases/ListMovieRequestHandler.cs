using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieService.Application.Dtos.Requests;
using MovieService.Application.Dtos.Responses;
using MovieService.Domain.AggregatesModels.Movie;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieService.Application.UseCases
{
    public  class ListMovieRequestHandler : IRequestHandler<ListMoviesRequest, BaseResponse<List<MovieDto>>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<ListMovieRequestHandler> _logger;

        public ListMovieRequestHandler(IMovieRepository movieRepository, ILogger<ListMovieRequestHandler> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }
        public async Task<BaseResponse<List<MovieDto>>> Handle(ListMoviesRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<List<MovieDto>> response = new BaseResponse<List<MovieDto>>();

            try
            {
                var movie = await _movieRepository.GetList(request.Page,request.Offset);
                response.Data = movie.Adapt<List<MovieDto>>();
            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                _logger.LogError("--- Listing Movie Error");

            }
            return response;
        }
    }
}
