namespace ExtendFile.Panelis.Application.Interfaces.Clients;

public interface IFileListClient
{
    Task<UploadFileToFolderResult> UploadFileToFolderAsync(
        string folder,
        string fileName,
        Stream fileStream,
        string contentType,
        CancellationToken cancellationToken = default);

    Task DeleteFileFromFolderAsync(
        string folder,
        string fileName,
        CancellationToken cancellationToken = default);
}

public record UploadFileToFolderResult(string Key, string Url);
