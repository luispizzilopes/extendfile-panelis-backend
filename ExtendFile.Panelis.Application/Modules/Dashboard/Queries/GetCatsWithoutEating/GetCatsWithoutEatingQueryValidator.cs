using FluentValidation;
using ExtendFile.Panelis.Application.Modules.Dashboard.Queries.GetCatsWithoutEating;

namespace ExtendFile.Panelis.Application.Modules.Dashboard.Queries.GetCatsWithoutEating;

public class GetCatsWithoutEatingQueryValidator : AbstractValidator<GetCatsWithoutEatingQuery>
{
    public GetCatsWithoutEatingQueryValidator()
    {
        RuleFor(x => x.Request)
            .NotNull()
            .WithMessage("Request é obrigatório.");
    }
}
