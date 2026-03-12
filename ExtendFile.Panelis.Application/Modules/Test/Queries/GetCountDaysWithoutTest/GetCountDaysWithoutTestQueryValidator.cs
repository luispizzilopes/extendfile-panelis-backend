using ExtendFile.Panelis.Application.Modules.Test.Requests.GetCountDaysWithoutTest;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Test.Queries.GetCountDaysWithoutTest;

public class GetCountDaysWithoutTestQueryValidator : AbstractValidator<GetCountDaysWithoutTestRequest>
{
    public GetCountDaysWithoutTestQueryValidator()
    {
        RuleFor(x => x.BoxId)
            .NotEmpty()
            .WithMessage("O ID do box é obrigatório");
    }
}