using FluentValidation;

namespace TodoCleanArchitecture.Application.Features.Todos.Commands.Create;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(x => x.Note)
            .MaximumLength(1000);
    }
}