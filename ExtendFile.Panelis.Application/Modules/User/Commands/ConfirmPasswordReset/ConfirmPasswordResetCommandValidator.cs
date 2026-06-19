using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.ConfirmPasswordReset;

public class ConfirmPasswordResetCommandValidator : AbstractValidator<ConfirmPasswordResetCommand>
{
    public ConfirmPasswordResetCommandValidator()
    {
        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("E-mail é obrigatório")
            .EmailAddress().WithMessage("E-mail inválido");

        RuleFor(x => x.Request.Code)
            .NotEmpty().WithMessage("Código é obrigatório")
            .Length(6).WithMessage("Código deve ter 6 dígitos")
            .Matches(@"^\d{6}$").WithMessage("Código deve conter apenas números");

        RuleFor(x => x.Request.NewPassword)
            .NotEmpty().WithMessage("Nova senha é obrigatória")
            .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres")
            .Matches(@"[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula")
            .Matches(@"[a-z]").WithMessage("A senha deve conter ao menos uma letra minúscula")
            .Matches(@"\d").WithMessage("A senha deve conter ao menos um número")
            .Matches(@"[!@#$%^&*(),.?"":{}|<>]").WithMessage("A senha deve conter ao menos um caractere especial");
    }
}
