using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using MovieService.Application.Dtos.Requests;
using MovieService.Application.Dtos.Responses;
using MovieService.Domain.AggregatesModels.Movie;
using MovieService.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MovieService.Application.UseCases
{
    public class GetMovieRequestHandler : IRequestHandler<GetMovieRequest, BaseResponse<MovieDto>>
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<GetMovieRequestHandler> _logger;

        public GetMovieRequestHandler(IMovieRepository movieRepository, ILogger<GetMovieRequestHandler> logger, IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _movieRepository = movieRepository;
            _logger = logger;
        }
        public async Task<BaseResponse<MovieDto>> Handle(GetMovieRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<MovieDto> response = new BaseResponse<MovieDto>();

            try
            {
                Movie movie = null;
                var movieCache = await _distributedCache.GetStringAsync(request.Id.ToString());

                if (movieCache == null)
                {
                    movie = await _movieRepository.Get(request.Id);
                    if (movie!=null)
                    {
                        await _distributedCache.SetStringAsync(movie.Id.ToString(), JsonConvert.SerializeObject(movie));
                    }
                    else
                    {
                        throw new MovieServiceDomainException("Movie not found.");
                    }
                }
                else
                {
                    movie = JsonConvert.DeserializeObject<Movie>(movieCache);
                }

                response.Data = movie.Adapt<MovieDto>();

            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message);
                _logger.LogError("--- Getting Movie Error", "Movie Id:" + request.Id);


            }
            return response;
        }
    }
}
