using MediatR;
using TodoCleanArchitecture.Application.Models.Common;

namespace TodoCleanArchitecture.Application.Features.Todos.Queries.GetAll;

public record GetAllQuery(int PageNumber, int PageSize) : IRequest<OperationResult<PaginatedList<GetAllQueryResult>>>;


