
using MediatR;
using TodoCleanArchitecture.Application.Abstractions.Repositories;
using TodoCleanArchitecture.Application.Models.Common;

namespace TodoCleanArchitecture.Application.Features.Todos.Commands.Delete;

internal class DeleteCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCommand, OperationResult<bool>>
{
    public async Task<OperationResult<bool>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.TodoItemRepository.DeleteAsync(request.Id);

        return OperationResult<bool>.SuccessResult(true);
    }
}