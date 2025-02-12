using MediatR; 
using Microsoft.Extensions.Logging; 
using TodoCleanArchitecture.Application.Models.Common;

namespace TodoCleanArchitecture.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class
    where TRequest : IRequest<TResponse>
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            var response = await next();
            return response;
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);

            if (typeof(TResponse).GetGenericTypeDefinition() == typeof(OperationResult<>))
            {
                var response = new OperationResult<TResponse> { IsException = true };

                return response as TResponse;
            }

            return default;
        }
    }
}