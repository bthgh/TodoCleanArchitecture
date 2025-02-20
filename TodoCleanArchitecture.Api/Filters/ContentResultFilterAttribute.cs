using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoCleanArchitecture.Application.Models.ApiResult;

namespace TodoCleanArchitecture.Api.Filters;

public class ContentResultFilterAttribute : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is not ContentResult contentResult) return;
        var apiResult = new ApiResult(true, ApiResultStatusCode.Success, contentResult.Content);
        context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
    }
}