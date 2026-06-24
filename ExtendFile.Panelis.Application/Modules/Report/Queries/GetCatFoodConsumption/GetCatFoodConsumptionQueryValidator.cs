using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Report.Queries.GetCatFoodConsumption;

public class GetCatFoodConsumptionQueryValidator : AbstractValidator<GetCatFoodConsumptionQuery>
{
    public GetCatFoodConsumptionQueryValidator()
    {
        RuleFor(x => x.Request.CatId)
            .NotEmpty()
            .WithMessage("ID do gato é obrigatório");

        RuleFor(x => x.Request.StartDate)
            .NotEmpty()
            .WithMessage("Data de início é obrigatória");

        RuleFor(x => x.Request.EndDate)
            .NotEmpty()
            .WithMessage("Data de fim é obrigatória");

        RuleFor(x => x)
            .Must(x => x.Request.StartDate <= x.Request.EndDate)
            .WithMessage("Data de início deve ser menor ou igual à data de fim");
    }
}
