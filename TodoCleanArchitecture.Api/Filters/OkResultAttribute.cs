﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoCleanArchitecture.Application.Models.ApiResult;

namespace TodoCleanArchitecture.Api.Filters;

public class OkResultAttribute:ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        switch (context.Result)
        {
            case OkObjectResult okObjectResult:
            {
                var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, okObjectResult.Value);
                context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
                break;
            }
            case OkResult okResult:
            {
                var apiResult = new ApiResult(true, ApiResultStatusCode.Success);
                context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
                break;
            }
            default:return;
        }
    }
}