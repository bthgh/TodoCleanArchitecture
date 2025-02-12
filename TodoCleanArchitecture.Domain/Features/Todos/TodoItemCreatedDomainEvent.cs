using TodoCleanArchitecture.Domain.Common;

namespace TodoCleanArchitecture.Domain.Features.Todos;

public sealed record TodoItemCreatedDomainEvent(int TodoItemId) : IDomainEvent;