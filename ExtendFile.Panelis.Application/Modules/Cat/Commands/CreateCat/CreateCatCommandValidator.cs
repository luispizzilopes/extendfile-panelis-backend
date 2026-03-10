using ExtendFile.Panelis.Application.Modules.Cat.Requests.CreateCat;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Cat.Commands.CreateCat;

public class CreateCatCommandValidator : AbstractValidator<CreateCatCommand>
{
    public CreateCatCommandValidator()
    {
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

        RuleFor(x => x.Request.DateOfBirth)
            .NotEmpty()
            .WithMessage("Data de nascimento é obrigatória")
            .LessThan(DateTime.Now)
            .WithMessage("Data de nascimento não pode ser futura")
            .GreaterThan(DateTime.Now.AddYears(-30))
            .WithMessage("Data de nascimento não pode ser superior a 30 anos");

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
