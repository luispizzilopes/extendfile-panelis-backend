using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.DisableTwoFactor;

public class DisableTwoFactorCommandValidator : AbstractValidator<DisableTwoFactorCommand>
{
    public DisableTwoFactorCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-mail é obrigatório")
            .EmailAddress().WithMessage("E-mail inválido");
    }
}
