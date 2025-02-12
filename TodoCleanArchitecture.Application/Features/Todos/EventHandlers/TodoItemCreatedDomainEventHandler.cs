using MediatR;
using Microsoft.Extensions.Logging;
using TodoCleanArchitecture.Domain.Features.Todos;

namespace TodoCleanArchitecture.Application.Features.Todos.EventHandlers;

public class TodoItemCreatedDomainEventHandler : INotificationHandler<TodoItemCreatedDomainEvent>
{
    private readonly ILogger<TodoItemCreatedDomainEventHandler> _logger;

    public TodoItemCreatedDomainEventHandler(ILogger<TodoItemCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("TodoCleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}