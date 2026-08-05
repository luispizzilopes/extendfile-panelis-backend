using System.Text.RegularExpressions;
using ErrorOr;
using ExtendFile.Panelis.Application.Interfaces.Clients;
using ExtendFile.Panelis.Application.Modules.House.Requests.UploadBoxDocument;
using ExtendFile.Panelis.Application.Modules.House.Responses.UploadBoxDocument;
using ExtendFile.Panelis.Domain.Interfaces.UnitOfWork;

namespace ExtendFile.Panelis.Application.Modules.House.UseCases.UploadBoxDocument;

public class UploadBoxDocumentUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileListClient _fileListClient;

    public UploadBoxDocumentUseCase(IUnitOfWork unitOfWork, IFileListClient fileListClient)
    {
        _unitOfWork = unitOfWork;
        _fileListClient = fileListClient;
    }

    public async Task<ErrorOr<UploadBoxDocumentResponse>> ExecuteAsync(
        UploadBoxDocumentRequest request,
        CancellationToken cancellationToken = default)
    {
        var house = await _unitOfWork.HouseRepository
            .GetHouseByIdAsync(request.HouseId, cancellationToken);

        if (house is null)
            return Error.NotFound(description: "Casa/Prédio não encontrada");

        var box = house.Boxes.FirstOrDefault(b => b.Id.Value == request.BoxId);

        if (box is null)
            return Error.NotFound(description: "Box não encontrado");

        var houseName = Sanitize(house.Name);
        var boxName = Sanitize(box.Name);
        var folder = $"{houseName}/{boxName}";
        var fileName = Path.GetFileName(request.File.FileName);

        try
        {
            await using var stream = request.File.OpenReadStream();
            var result = await _fileListClient.UploadFileToFolderAsync(
                folder,
                fileName,
                stream,
                request.File.ContentType,
                cancellationToken);

            return new UploadBoxDocumentResponse { Key = result.Key, Url = result.Url };
        }
        catch (Exception)
        {
            return Error.Failure(description: "Erro ao enviar documento ao serviço de arquivos");
        }
    }

    private static string Sanitize(string name)
        => Regex.Replace(name, @"[^a-zA-Z0-9\-_]", "-");
}
