using AutoMapper;
using MediatR;
using TodoCleanArchitecture.Application.Abstractions.Repositories;
using TodoCleanArchitecture.Application.Models.Common;
using TodoCleanArchitecture.Domain.Features.Todos;
using TodoCleanArchitecture.Application.Extensions;

namespace TodoCleanArchitecture.Application.Features.Todos.Queries.GetAll;

internal class GetAllQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
    : IRequestHandler<GetAllQuery, OperationResult<PaginatedList<GetAllQueryResult>>>
{
    public async Task<OperationResult<PaginatedList<GetAllQueryResult>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var todoItemsList = await unitOfWork.TodoItemRepository.GetAllAsync(request.PageNumber,request.PageSize);
        
        return OperationResult<PaginatedList<GetAllQueryResult>>
            .SuccessResult(mapper.Map<TodoItem, GetAllQueryResult>(todoItemsList));
    }
}

