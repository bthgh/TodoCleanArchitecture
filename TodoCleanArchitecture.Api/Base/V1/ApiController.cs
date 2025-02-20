using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoCleanArchitecture.Api.Filters;
using TodoCleanArchitecture.Application.Models.Common;

namespace TodoCleanArchitecture.Api.Base.V1;

[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    protected IActionResult OperationResult<TModel>(OperationResult<TModel> result)
    {
        if (result is null)
            return new ServerErrorResult("Server Error");


        if (result.IsSuccess)
            return result.Result is bool ? Ok() : Ok(result.Result);

        if (result.IsNotFound)
        {

            AddErrors(result);

            var notFoundErrors = new ValidationProblemDetails(ModelState);

            return NotFound(notFoundErrors.Errors);
        }

        AddErrors(result);

        var badRequestErrors = new ValidationProblemDetails(ModelState);

        return BadRequest(badRequestErrors.Errors);

    }

    private void AddErrors<TModel>(OperationResult<TModel> result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    } 
}
