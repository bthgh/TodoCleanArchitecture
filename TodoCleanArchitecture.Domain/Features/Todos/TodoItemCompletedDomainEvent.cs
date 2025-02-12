using TodoCleanArchitecture.Domain.Common;

namespace TodoCleanArchitecture.Domain.Features.Todos;

public sealed record TodoItemCompletedDomainEvent(int TodoItemId) : IDomainEvent;