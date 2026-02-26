using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.UpdateBox;

public class UpdateBoxCommandValidator : AbstractValidator<UpdateBoxCommand>
{
    public UpdateBoxCommandValidator()
    {
        RuleFor(x => x.Request.Id)
            .NotEmpty()
            .WithMessage("Id é obrigatório");

        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage("Nome é obrigatório")
            .MaximumLength(200)
            .WithMessage("Nome não pode exceder 200 caracteres");
    }
}
