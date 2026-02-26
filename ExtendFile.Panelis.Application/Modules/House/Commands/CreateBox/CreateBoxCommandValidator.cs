using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.CreateBox;

public class CreateBoxCommandValidator : AbstractValidator<CreateBoxCommand>
{
    public CreateBoxCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage("Nome é obrigatório")
            .MaximumLength(200)
            .WithMessage("Nome não pode exceder 200 caracteres");
    }
}