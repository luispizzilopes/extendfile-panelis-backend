using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.House.Commands.DeleteBoxDocument;

public class DeleteBoxDocumentCommandValidator : AbstractValidator<DeleteBoxDocumentCommand>
{
    public DeleteBoxDocumentCommandValidator()
    {
        RuleFor(x => x.Request.HouseId)
            .NotEmpty()
            .WithMessage("Id do prédio é obrigatório");

        RuleFor(x => x.Request.BoxId)
            .NotEmpty()
            .WithMessage("Id do box é obrigatório");

        RuleFor(x => x.Request.FileName)
            .NotEmpty()
            .WithMessage("Nome do arquivo é obrigatório");
    }
}
