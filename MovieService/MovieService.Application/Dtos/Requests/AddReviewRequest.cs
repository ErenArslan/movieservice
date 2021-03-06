﻿using MediatR;
using MovieService.Application.Dtos.Responses;
using System;

namespace MovieService.Application.Dtos.Requests
{
    public class AddReviewRequest : IRequest<BaseResponse<bool>>
    {
        public int Rating { get; set; }
        public string Note { get; set; }
        public int MovieId { get; set; }
        public Guid UserId { get; set; }
    }
}
