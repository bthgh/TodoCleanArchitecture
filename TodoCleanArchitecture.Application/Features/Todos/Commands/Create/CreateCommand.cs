using MediatR;
using TodoCleanArchitecture.Application.Models.Common;
using TodoCleanArchitecture.Domain.Features.Todos;

namespace TodoCleanArchitecture.Application.Features.Todos.Commands.Create;


public record CreateCommand(string Title,
    string? Note,
    Priority Priority,
    DateTime? DueDate) : IRequest<OperationResult<bool>>;
