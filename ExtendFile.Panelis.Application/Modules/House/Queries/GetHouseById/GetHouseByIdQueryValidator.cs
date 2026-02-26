using ExtendFile.Panelis.Application.Modules.House.Requests.GetHouseById;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.House.Queries.GetHouseById;

public class GetHouseByIdQueryValidator : AbstractValidator<GetHouseByIdQuery>
{
    public GetHouseByIdQueryValidator()
    {
        RuleFor(x => x.Request.Id)
            .NotEmpty()
            .WithMessage("Id é obrigatório");
    }
}