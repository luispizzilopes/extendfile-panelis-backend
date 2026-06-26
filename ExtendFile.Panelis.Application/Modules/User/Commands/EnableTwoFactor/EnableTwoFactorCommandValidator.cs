using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.EnableTwoFactor;

public class EnableTwoFactorCommandValidator : AbstractValidator<EnableTwoFactorCommand>
{
    public EnableTwoFactorCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-mail é obrigatório")
            .EmailAddress().WithMessage("E-mail inválido");
    }
}
