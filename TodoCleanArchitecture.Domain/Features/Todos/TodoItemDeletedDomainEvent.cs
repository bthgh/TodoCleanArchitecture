using TodoCleanArchitecture.Domain.Common;

namespace TodoCleanArchitecture.Domain.Features.Todos;

public sealed record TodoItemDeletedDomainEvent(int TodoItemId) : IDomainEvent;