using ExtendFile.Panelis.Application.Modules.Test.Requests.GetTestLinesByTestId;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetTestLinesByTestId;

public class GetTestLinesByTestIdQueryValidator : AbstractValidator<GetTestLinesByTestIdQuery>
{
    public GetTestLinesByTestIdQueryValidator()
    {
        RuleFor(x => x.Request.TestId)
            .NotEmpty()
            .WithMessage("O ID do teste é obrigatório");
    }
}
