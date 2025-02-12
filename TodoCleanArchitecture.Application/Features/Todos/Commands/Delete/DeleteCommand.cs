using MediatR;
using TodoCleanArchitecture.Application.Models.Common;

namespace TodoCleanArchitecture.Application.Features.Todos.Commands.Delete;
 
public record DeleteCommand(int Id) : IRequest<OperationResult<bool>>;