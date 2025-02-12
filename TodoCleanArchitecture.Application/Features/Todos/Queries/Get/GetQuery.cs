using MediatR;
using TodoCleanArchitecture.Application.Models.Common;

namespace TodoCleanArchitecture.Application.Features.Todos.Queries.Get;

public record GetQuery(int Id) : IRequest<OperationResult<GetQueryResult>>;