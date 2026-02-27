using ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCats;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCats;

public class GetCatsQueryValidator : AbstractValidator<GetCatsQuery>
{
    public GetCatsQueryValidator()
    {
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
