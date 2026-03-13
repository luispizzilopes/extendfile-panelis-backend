using FluentValidation;
using ExtendFile.Panelis.Application.Modules.Test.Commands.DeleteTest;

namespace ExtendFile.Panelis.Application.Modules.Test.Commands.DeleteTest;

public class DeleteTestCommandValidator : AbstractValidator<DeleteTestCommand>
{
    public DeleteTestCommandValidator()
    {
        RuleFor(x => x.Request)
            .NotNull()
            .WithMessage("Request é obrigatório.");

        RuleFor(x => x.Request.TestId)
            .NotEmpty()
            .WithMessage("Id do teste é obrigatório.");
    }
}
