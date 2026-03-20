using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByCatWithoutEating;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByCatWithoutEating;

public class GetTestLinesByCatWithoutEatingQueryValidator : AbstractValidator<GetTestLinesByCatWithoutEatingRequest>
{
    public GetTestLinesByCatWithoutEatingQueryValidator()
    {
        RuleFor(x => x.CatId)
            .NotEmpty()
            .WithMessage("O ID do gato é obrigatório");
    }
}
