
using MediatR;
using TodoCleanArchitecture.Application.Abstractions.Repositories;
using TodoCleanArchitecture.Application.Models.Common;

namespace TodoCleanArchitecture.Application.Features.Todos.Commands.Update;

internal class UpdateCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCommand, OperationResult<bool>>
{
    public async Task<OperationResult<bool>> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await unitOfWork.TodoItemRepository.GetByIdAsync(request.Id,true);

        if (todoItem is null)
            return OperationResult<bool>.NotFoundResult("Specified Todo Item not found");

        todoItem.Title = request.Title;
        todoItem.Note = request.Note;
        todoItem.Priority = request.Priority;
        todoItem.DueDate = request.DueDate;
        
        await unitOfWork.CommitAsync();

        return OperationResult<bool>.SuccessResult(true);
    }
}