using ExtendFile.Panelis.Application.Modules.House.Requests.DeleteHouse;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.DeleteHouse;

public class DeleteHouseCommandValidator : AbstractValidator<DeleteHouseCommand>
{
    public DeleteHouseCommandValidator()
    {
        RuleFor(x => x.Request.Id)
            .NotEmpty()
            .WithMessage("Id é obrigatório");
    }
}
