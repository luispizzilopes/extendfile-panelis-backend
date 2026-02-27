using ExtendFile.Panelis.Application.Modules.Cat.Requests.GetCatById;
using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Cat.Queries.GetCatById;

public class GetCatByIdQueryValidator : AbstractValidator<GetCatByIdQuery>
{
    public GetCatByIdQueryValidator()
    {
        RuleFor(x => x.Request.Id)
            .NotEmpty()
            .WithMessage("Id é obrigatório");
    }
}
