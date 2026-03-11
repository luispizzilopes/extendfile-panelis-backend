using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestsByBoxId;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestsByBoxId;

public class GetTestsByBoxIdQueryValidator : AbstractValidator<GetTestsByBoxIdQuery>
{
    public GetTestsByBoxIdQueryValidator()
    {
        RuleFor(x => x.Request.BoxId)
            .NotEmpty()
            .WithMessage("BoxId é obrigatório");

        RuleFor(x => x.Request.PaginationParams.PageNumber)
            .GreaterThan(0)
            .WithMessage("Número da página deve ser maior que 0");

        RuleFor(x => x.Request.PaginationParams.PageSize)
            .GreaterThan(0)
            .WithMessage("Tamanho da página deve ser maior que 0")
            .LessThan(200)
            .WithMessage("Tamanho da página não pode exceder 200");
    }
}
