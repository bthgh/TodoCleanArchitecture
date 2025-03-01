﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoCleanArchitecture.Application.Extensions;
using TodoCleanArchitecture.Application.Models.ApiResult;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace TodoCleanArchitecture.Api.Filters;

public class ModelStateValidationAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var contextActionArgument in context.ActionArguments.Values)
        {
            var viewModelValidator =
                context.HttpContext.RequestServices.GetService(
                    typeof(IValidator<>).MakeGenericType(contextActionArgument.GetType()));

            if (viewModelValidator is IValidator validator)
            {
                var validationResult =await validator.ValidateAsync(new ValidationContext<object>(contextActionArgument));

                if (!validationResult.IsValid)
                {
                    foreach (var validationResultError in validationResult.Errors)
                    {
                        context.ModelState.AddModelError(validationResultError.PropertyName, validationResultError.ErrorMessage);
                    }
                }
            }
        }

        var modelState = context.ModelState;

        if (!modelState.IsValid)
        {

            var model = context.ActionArguments.FirstOrDefault().Value;

            if (model != null)
            {
                var errors = new ValidationProblemDetails(modelState);

                var message = ApiResultStatusCode.BadRequest.ToDisplay();

                var apiResult = new ApiResult<IDictionary<string, string[]>>(false, ApiResultStatusCode.BadRequest, errors.Errors, message);
                context.Result = new JsonResult(apiResult) { StatusCode = StatusCodes.Status400BadRequest };
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            else
            {
                var apiResult = new ApiResult(false, ApiResultStatusCode.BadRequest);
                context.Result = new JsonResult(apiResult) { StatusCode = 400 };
                context.HttpContext.Response.StatusCode = 400;
            }
        }

        await base.OnActionExecutionAsync(context, next);
    }
}