using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieService.Application.Dtos.Responses;
using System.Linq;
using System.Threading.Tasks;
namespace MovieService.Api.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {

                var validationErrors = context.ModelState
                    .Keys
                    .SelectMany(k => context.ModelState[k].Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();


                var response = new BaseResponse<bool>();

                response.Errors.AddRange(validationErrors);

                context.Result = new BadRequestObjectResult(response);
                return;
            }
            await next();
        }
    }
}
