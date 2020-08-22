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
    public class AddMovieRequestHandler : IRequestHandler<AddMovieRequest, BaseResponse<bool>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<AddMovieRequestHandler> _logger;
        public AddMovieRequestHandler(IMovieRepository movieRepository, ILogger<AddMovieRequestHandler> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<bool>> Handle(AddMovieRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                var movie = new Movie(request.Id, request.Title, request.Overview, request.ReleaseDate);
                movie.SetBackDropPath(request.BackdropPath);
                movie.SetIsAdult(request.IsAdult);
                movie.SetOrginalLanguage(request.OriginalLanguage);
                movie.SetOriginalTitle(request.OriginalTitle);
                movie.SetPosterPath(request.PosterPath);
                movie.SetPopularity(request.Popularity);

                await _movieRepository.AddMovie(movie);
              

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
