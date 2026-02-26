using ExtendFile.Panelis.Application.Modules.House.Requests.UpdateHouse;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.UpdateHouse;

public class UpdateHouseCommandValidator : AbstractValidator<UpdateHouseCommand>
{
    public UpdateHouseCommandValidator()
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
