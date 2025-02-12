using FluentValidation;

namespace TodoCleanArchitecture.Application.Features.Todos.Commands.Delete;

public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1);
    }
}