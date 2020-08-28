using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieService.Api.Services;
using MovieService.Application.Dtos.Requests;
using System;
using System.Threading.Tasks;

namespace MovieService.Api.Controllers
{
  
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        public MovieController(IMediator mediator, IIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpGet("GetMovie/{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            if (id <= 0) return BadRequest();

            var getMovieRequest = new GetMovieRequest() { Id = id };
            var response = await _mediator.Send(getMovieRequest);

            if (!response.HasError)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("ListMovie")]
        public async Task<IActionResult> ListMovie([FromQuery(Name = "page")] int page, [FromQuery(Name = "offset")] int offset)
        {

            var listMovieRequest = new ListMoviesRequest() { Page = page, Offset = offset };
            var response = await _mediator.Send(listMovieRequest);

            if (!response.HasError)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview([FromBody]AddReviewRequest request)
        {
            // If user logged in get id from token.
            //  var userId = _identityService.GetUserIdentity();
            var userId = Guid.NewGuid();
            request.UserId = userId;
            var response = await _mediator.Send(request);

            if (!response.HasError)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("RecommendMovie")]
        public async Task<IActionResult> RecommendMovie([FromBody]RecommendMovieRequest request)
        {

            var response = await _mediator.Send(request);

            if (!response.HasError)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

    }
}
