using FluentValidation;

namespace TodoCleanArchitecture.Application.Features.Todos.Commands.Update;

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(x => x.Note)
            .MaximumLength(1000);
        
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1);
    }
}