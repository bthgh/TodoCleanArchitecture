using TodoCleanArchitecture.Domain.Features.Todos;

namespace TodoCleanArchitecture.Application.Features.Todos.Queries.GetAll;
public record GetAllQueryResult(
    int Id,
    string Title,
    string? Note,
    Priority Priority,
    DateTime? DueDate,
    bool IsCompleted,
    DateTime? CompletedAt,
    DateTimeOffset CreatedAt,
    string? CreatedBy,
    DateTimeOffset ModifiedAt,
    string? ModifiedBy);

 