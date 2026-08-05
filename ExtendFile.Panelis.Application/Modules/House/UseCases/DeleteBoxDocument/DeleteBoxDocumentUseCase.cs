using System.Text.RegularExpressions;
using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Clients;
using ExtendFile.Panelis.Application.Modules.House.Requests.DeleteBoxDocument;
using ExtendFile.Panelis.Application.Modules.House.Responses.DeleteBoxDocument;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.DeleteBoxDocument;

public class DeleteBoxDocumentUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileListClient _fileListClient;

    public DeleteBoxDocumentUseCase(IUnitOfWork unitOfWork, IFileListClient fileListClient)
    {
        _unitOfWork = unitOfWork;
        _fileListClient = fileListClient;
    }

    public async Task<ErrorOr<DeleteBoxDocumentResponse>> ExecuteAsync(
        DeleteBoxDocumentRequest request,
        CancellationToken cancellationToken = default)
    {
        var house = await _unitOfWork.HouseRepository
            .GetHouseByIdAsync(request.HouseId, cancellationToken);

        if (house is null)
            return Error.NotFound(description: "Casa/Prédio não encontrada");

        var box = house.Boxes.FirstOrDefault(b => b.Id.Value == request.BoxId);

        if (box is null)
            return Error.NotFound(description: "Box não encontrado");

        var folder = $"{Sanitize(house.Name)}/{Sanitize(box.Name)}";

        try
        {
            await _fileListClient.DeleteFileFromFolderAsync(folder, request.FileName, cancellationToken);
            return new DeleteBoxDocumentResponse { Success = true };
        }
        catch (Exception)
        {
            return Error.Failure(description: "Erro ao remover documento do serviço de arquivos");
        }
    }

    private static string Sanitize(string name)
        => Regex.Replace(name, @"[^a-zA-Z0-9\-_]", "-");
}
