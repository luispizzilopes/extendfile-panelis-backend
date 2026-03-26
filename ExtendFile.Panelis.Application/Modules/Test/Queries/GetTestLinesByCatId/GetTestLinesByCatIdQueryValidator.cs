using FluentValidation;
using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByCatId;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByCatId;

public class GetTestLinesByCatIdQueryValidator : AbstractValidator<GetTestLinesByCatIdQuery>
{
    public GetTestLinesByCatIdQueryValidator()
    {
        RuleFor(x => x.Request.CatId)
            .NotEmpty()
            .WithMessage("CatId é obrigatório");
    }
}
