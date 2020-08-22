using MediatR;
using MovieService.Application.Dtos.Responses;
using System;

namespace MovieService.Application.Dtos.Requests
{
    public class AddMovieRequest:IRequest<BaseResponse<bool>>
    {
        public int Id { get; set; }
        public double Popularity { get;  set; }
        public string PosterPath { get;  set; }
        public bool IsAdult { get;  set; }
        public string BackdropPath { get;  set; }
        public string OriginalLanguage { get;  set; }
        public string OriginalTitle { get;  set; }
        public string Title { get;  set; }
        public string Overview { get;  set; }
        public DateTime ReleaseDate { get;  set; }
    }
}
