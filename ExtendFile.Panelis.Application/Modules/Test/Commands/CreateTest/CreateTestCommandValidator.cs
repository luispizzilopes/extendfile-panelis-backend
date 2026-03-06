using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Test.Commands.CreateTest;

public class CreateTestCommandValidator : AbstractValidator<CreateTestCommand>
{
    public CreateTestCommandValidator()
    {
        RuleFor(x => x.Request.File)
            .NotNull()
            .WithMessage("O arquivo não pode ser nulo");
    }
}