using AutoMapper;
using MediatR;
using TodoCleanArchitecture.Application.Abstractions.Repositories;
using TodoCleanArchitecture.Application.Models.Common;

namespace TodoCleanArchitecture.Application.Features.Todos.Queries.Get;

internal class GetQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
    : IRequestHandler<GetQuery, OperationResult<GetQueryResult>>
{
    public async Task<OperationResult<GetQueryResult>> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        var todoItem = await unitOfWork.TodoItemRepository.GetByIdAsync(request.Id);

        if (todoItem is null)
            return OperationResult<GetQueryResult>.NotFoundResult("Todo Item is not Exist!");

        var result = mapper.Map<GetQueryResult>(todoItem);

        return OperationResult<GetQueryResult>.SuccessResult(result);
    }
}

