using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.UploadBoxDocument;

public class UploadBoxDocumentCommandValidator : AbstractValidator<UploadBoxDocumentCommand>
{
    public UploadBoxDocumentCommandValidator()
    {
        RuleFor(x => x.Request.HouseId)
            .NotEmpty()
            .WithMessage("Id do prédio é obrigatório");

        RuleFor(x => x.Request.BoxId)
            .NotEmpty()
            .WithMessage("Id do box é obrigatório");

        RuleFor(x => x.Request.File)
            .NotNull()
            .WithMessage("Arquivo é obrigatório");
    }
}
