using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.User.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Request.CurrentPassword)
            .NotEmpty()
            .WithMessage("Senha atual é obrigatória");

        RuleFor(x => x.Request.NewPassword)
            .NotEmpty()
            .WithMessage("Nova senha é obrigatória")
            .MinimumLength(6)
            .WithMessage("Nova senha deve ter no mínimo 6 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório")
            .EmailAddress()
            .WithMessage("Email inválido");
    }
}
