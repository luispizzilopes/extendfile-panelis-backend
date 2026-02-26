using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.DeleteBox;

public class DeleteBoxCommandValidator : AbstractValidator<DeleteBoxCommand>
{
    public DeleteBoxCommandValidator()
    {
        RuleFor(x => x.Request.Id)
            .NotEmpty()
            .WithMessage("Id é obrigatório");
    }
}
