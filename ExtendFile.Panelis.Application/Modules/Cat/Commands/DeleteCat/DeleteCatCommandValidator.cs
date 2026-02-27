using ExtendFile.Panelis.Application.Modules.Cat.Requests.DeleteCat;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Cat.Commands.DeleteCat;

public class DeleteCatCommandValidator : AbstractValidator<DeleteCatCommand>
{
    public DeleteCatCommandValidator()
    {
        RuleFor(x => x.Request.Id)
            .NotEmpty()
            .WithMessage("Id é obrigatório");
    }
}
