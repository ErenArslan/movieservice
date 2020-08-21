using System;

namespace MovieService.Application.Dtos.Responses
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Note { get; set; }
        public Guid UserId { get; set; }
    }
}
