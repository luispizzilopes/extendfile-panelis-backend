using FluentValidation;
using ExtendFile.Panelis.Application.Modules.Setting.Commands.UpsertSetting;
using ExtendFile.Panelis.Application.Modules.Setting.Requests.UpsertSetting;

namespace ExtendFile.Panelis.Application.Modules.Setting.Commands.UpsertSetting;

public class UpsertSettingCommandValidator : AbstractValidator<UpsertSettingCommand>
{
    public UpsertSettingCommandValidator()
    {
        RuleFor(x => x.Request)
            .NotNull()
            .WithMessage("Request é obrigatório.");

        RuleFor(x => x.Request.LessThanEnoughThreshold)
            .NotEmpty()
            .WithMessage("Quantidade mínima é obrigatória.");

        RuleFor(x => x.Request.MoreThanEnoughThreshold)
            .NotEmpty()
            .WithMessage("Quantidade máxima é obrigatória.");
    }
}
