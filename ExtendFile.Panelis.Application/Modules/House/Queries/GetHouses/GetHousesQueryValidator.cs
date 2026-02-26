using ExtendFile.Panelis.Application.Modules.House.Requests.GetHouses;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.House.Queries.GetHouses;

public class GetHousesQueryValidator : AbstractValidator<GetHousesQuery>
{
    public GetHousesQueryValidator()
    {
        RuleFor(x => x.Request.PaginationParams.PageNumber)
            .GreaterThan(0)
            .WithMessage("Número da página deve ser maior que 0");

        RuleFor(x => x.Request.PaginationParams.PageSize)
            .GreaterThan(0)
            .WithMessage("Tamanho da página deve ser maior que 0")
            .LessThanOrEqualTo(200)
            .WithMessage("Tamanho da página deve ser menor ou igual a 200");
    }
}
