using MediatR;
using TodoCleanArchitecture.Application.Abstractions.Repositories;
using TodoCleanArchitecture.Application.Models.Common;
using TodoCleanArchitecture.Domain.Features.Todos;

namespace TodoCleanArchitecture.Application.Features.Todos.Commands.Create;

internal class CreateCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCommand, OperationResult<bool>>
{
    public async Task<OperationResult<bool>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var todoItem = new TodoItem()
        {
            Title = request.Title,
            Note = request.Note,
            Priority = request.Priority,
            DueDate = request.DueDate
        }; 
        
        await unitOfWork.TodoItemRepository.CreateAsync(todoItem);

        await unitOfWork.CommitAsync();

        return OperationResult<bool>.SuccessResult(true);
    }
}