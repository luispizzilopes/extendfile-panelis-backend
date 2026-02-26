using ExtendFile.Panelis.Application.Modules.House.Requests.CreateHouse;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.CreateHouse;

public class CreateHouseCommandValidator : AbstractValidator<CreateHouseCommand>
{
    public CreateHouseCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage("Nome é obrigatório")
            .MaximumLength(200)
            .WithMessage("Nome não pode exceder 200 caracteres");
    }
}
