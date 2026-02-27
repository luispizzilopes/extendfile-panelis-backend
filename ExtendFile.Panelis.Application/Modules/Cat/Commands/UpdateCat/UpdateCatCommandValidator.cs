using ExtendFile.Panelis.Application.Modules.Cat.Requests.UpdateCat;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Cat.Commands.UpdateCat;

public class UpdateCatCommandValidator : AbstractValidator<UpdateCatCommand>
{
    public UpdateCatCommandValidator()
    {
        RuleFor(x => x.Request.Id)
            .NotEmpty()
            .WithMessage("Id é obrigatório");

        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .WithMessage("Nome é obrigatório")
            .MaximumLength(200)
            .WithMessage("Nome não pode exceder 200 caracteres");

        RuleFor(x => x.Request.Hash)
            .NotEmpty()
            .WithMessage("Hash é obrigatório")
            .MaximumLength(500)
            .WithMessage("Hash não pode exceder 500 caracteres");

        RuleFor(x => x.Request.Age)
            .GreaterThan(0)
            .WithMessage("Idade deve ser maior que 0")
            .LessThan(30)
            .WithMessage("Idade não pode exceder 30 anos");

        RuleFor(x => x.Request.Weight)
            .GreaterThan(0)
            .WithMessage("Peso deve ser maior que 0")
            .LessThan(50)
            .WithMessage("Peso não pode exceder 50 kg");

        RuleFor(x => x.Request.BoxId)
            .NotEmpty()
            .WithMessage("BoxId é obrigatório");
    }
}
