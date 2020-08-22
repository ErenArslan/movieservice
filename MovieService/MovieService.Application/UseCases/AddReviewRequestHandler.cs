using MediatR;
using Microsoft.Extensions.Logging;
using MovieService.Application.Dtos.Requests;
using MovieService.Application.Dtos.Responses;
using MovieService.Domain.AggregatesModels.Movie;
using MovieService.Domain.SeedWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MovieService.Application.UseCases
{
    public  class AddReviewRequestHandler : IRequestHandler<AddReviewRequest, BaseResponse<bool>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<AddReviewRequestHandler> _logger;
        private readonly IGuidGenerator _guidGenerator;
        public AddReviewRequestHandler(IMovieRepository movieRepository, ILogger<AddReviewRequestHandler> logger, IGuidGenerator guidGenerator)
        {
            _movieRepository = movieRepository;
            _logger = logger;
            _guidGenerator = guidGenerator;
        }
        public async Task<BaseResponse<bool>> Handle(AddReviewRequest request, CancellationToken cancellationToken)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                var movie = await _movieRepository.Get(request.MovieId);
                if (movie==null)
                {
                    response.Errors.Add("Movie  not found");
                    return response;
                }
                var review = new Review(_guidGenerator.Create(), request.UserId, request.Note, request.Rating);
                movie.AddReview(review);

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
